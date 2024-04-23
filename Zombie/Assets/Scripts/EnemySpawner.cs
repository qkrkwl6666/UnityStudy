using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
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
    public ZombieData[] zombieDatas;
    private int wave; // 현재 웨이브

    private void Update() 
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

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() 
    {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemies.Count);
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

        var go = Instantiate(enemyPrefab, spawnPoints[randomNum].transform.position,
                spawnPoints[randomNum].transform.rotation);

        enemies.Add(go);

        go.Setup(data);

        go.onDeath += () =>
        {
            enemies.Remove(go);
            Destroy(go.gameObject, 1f);
            GameManager.instance.AddScore(100);
            if (Random.Range(0.0f, 1.0f) > 0.7)
            {
                GameManager.instance.GetComponent<ItemSpawner>().Spawn(go.transform);
            }
        };
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) 
    {
        int randomNum = Random.Range(0, 4);

        var go = Instantiate(enemyPrefab, spawnPoints[randomNum].transform.position,
                spawnPoints[randomNum].transform.rotation);

        enemies.Add(go);

        if(intensity == 0.0f)
            go.Setup(healthMin, damageMin, speedMin, Color.white);
        else
            go.Setup(intensity * healthMax, intensity * damageMax, intensity * speedMax, intensity > 0.7 ? strongEnemyColor : Color.white);

        go.onDeath += () =>
        {
            enemies.Remove(go);
            Destroy(go.gameObject, 1f);
            GameManager.instance.AddScore(Mathf.FloorToInt(100 + 100 * intensity));
            if (Random.Range(0.0f, 1.0f) > 0.7)
            {
                GameManager.instance.GetComponent<ItemSpawner>().Spawn(go.transform);
            }
        };

    }
}