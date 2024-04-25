using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviourPun, IPunObservable
{
    public Enemy enemyPrefab; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    public List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트

    private int enemyCount;
    public ZombieData[] zombieDatas;

    private int wave; // 현재 웨이브

    private void Awake()
    {
        PhotonPeer.RegisterType(typeof(Color), 128, ColorSerialization.SerializeColor,
            ColorSerialization.DeserializeColor);
    }

    private void Update() 
    {

        if (PhotonNetwork.IsMasterClient)
        {
            // 게임 오버 상태일때는 생성하지 않음
            if (GameManager.instance != null && GameManager.instance.isGameover)
            {
                return;
            }

            // 적을 모두 물리친 경우 다음 스폰 실행
            if (enemies.Count <= 0)
            {
                SpawnWave();
            }
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() 
    {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemyCount);
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() 
    {
        wave++;
        for (int i = 0; i < wave * 3; i++)
        {
            //CreateEnemy(Random.Range(0.0f , 1.0f));
            // 0.5 , 0.7, 1.0
            float percentage = Random.Range(0.0f, 1.0f);
            for (int j = 0; j < zombieDatas.Length; j++)
            {
                if (percentage <= zombieDatas[j].percentage)
                {
                    CreateEnemy(zombieDatas[j]);
                    break;
                }
            }

            //CreateEnemy(zombieDatas[Random.Range(0, zombieDatas.Length)]);
        }
    }

    private void CreateEnemy(ZombieData data)
    {
        int randomNum = Random.Range(0, 4);

        var go = PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoints[randomNum].transform.position,
                spawnPoints[randomNum].transform.rotation);

        var enemy = go.GetComponent<Enemy>();

        enemy.photonView.RPC("Setup", RpcTarget.All, data.health, data.damage, data.speed, data.skinColor);

        enemy.onDeath += () =>
        {
            enemyCount = enemies.Count;
            enemies.Remove(enemy);
            StartCoroutine(CoDestroyAfter(go, 5f));

            GameManager.instance.AddScore(100);
            if (Random.Range(0.0f, 1.0f) > 0.7)
            {
                GameManager.instance.GetComponent<ItemSpawner>().Spawn(enemy.transform);
            }
            UIManager.instance.UpdateWaveText(wave, enemyCount);
        };

        enemyCount++;
        enemies.Add(enemy);

        UIManager.instance.UpdateWaveText(wave, enemyCount);
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) 
    {
        int randomNum = Random.Range(0, 4);

        var go = PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoints[randomNum].transform.position,
                spawnPoints[randomNum].transform.rotation);

        var enemy = go.GetComponent<Enemy>();
        enemy.photonView.RPC("Setup", RpcTarget.All, intensity * healthMax, 
            intensity * damageMax, intensity * speedMax, intensity > 0.7 ? 
            strongEnemyColor : Color.white);

        enemy.onDeath += () =>
        {
            enemyCount = enemies.Count;
            //enemyCount--;
            enemies.Remove(enemy);
            StartCoroutine(CoDestroyAfter(go, 5f));
            GameManager.instance.AddScore(Mathf.FloorToInt(100 + 100 * intensity));
            if (Random.Range(0.0f, 1.0f) > 0.7)
            {
                GameManager.instance.GetComponent<ItemSpawner>().Spawn(enemy.transform);
            }
            UIManager.instance.UpdateWaveText(wave, enemyCount);
        };

        enemies.Add(enemy);
        enemyCount = enemies.Count;

        UIManager.instance.UpdateWaveText(wave, enemyCount);
    }

    IEnumerator CoDestroyAfter(GameObject go , float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(go);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(enemyCount);
            stream.SendNext(wave);
        }
        else
        {
            enemyCount = (int)stream.ReceiveNext();
            wave = (int)stream.ReceiveNext();
            UIManager.instance.UpdateWaveText(wave, enemyCount);
        }
    }
}