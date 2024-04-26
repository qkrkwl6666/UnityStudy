using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemysSpawner : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public Transform spawnPoint;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<GameObject> disableEnemies = new List<GameObject>();
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    private void Awake()
    {
        AddEnemyPrefabs();
    }
    private void Start()
    {
        EnemyInstantiate(0, 20);
        EnemyInstantiate(1, 20);
        EnemyInstantiate(2, 20);

        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (!PlayerInfo.isDead)
        {
            int index = Random.Range(0, disableEnemies.Count - 1);

            var randomPos = spawnPoint.position + Random.insideUnitSphere * 30f;
            randomPos.y = 0;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 50f, 3))
            {
                if (index < 0 || index >= disableEnemies.Count - 1) continue;

                disableEnemies[index].transform.position = hit.position;
                disableEnemies[index].SetActive(true);

                enemies.Add(disableEnemies[index]);
                disableEnemies.RemoveAt(index);
            }
            else
            {
                Debug.Log("Nav Mesh Not Found!!!");
            }

            yield return new WaitForSeconds(1.5f);
        }

        enemies.Clear();
        disableEnemies.Clear();
    }



    private void EnemyInstantiate(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(enemyPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
            go.GetComponent<Enemy>().SetUp(enemyDatas[index]);
            disableEnemies.Add(go);
        }
    }
    private void AddEnemyPrefabs()
    {
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.ZomBear));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Zombunny));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Hellephant));
    }

}
