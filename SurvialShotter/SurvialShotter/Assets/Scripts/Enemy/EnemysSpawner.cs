using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyData;

public class EnemysSpawner : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public Transform spawnPoint;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> disableEnemies = new List<GameObject>();
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    private void Awake()
    {
        AddEnemyPrefabs();
        //enemyDatas[(int)EnemyType.ZomBear]
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
            int randomNum = Random.Range(0, 101);
            int index = 0;
    
            if (randomNum <= enemyDatas[(int)EnemyType.ZomBear].percentage) index = 0;
            else if (randomNum <= enemyDatas[(int)EnemyType.Zombunny].percentage) index = 1;
            else if (randomNum <= enemyDatas[(int)EnemyType.Hellephant].percentage) index = 2;

            var go = disableEnemies.Find(enemy => enemy.GetComponent<Enemy>().enemyType == 
            (EnemyType)index);

            if(go == null)
            {
                go = EnemyInstantiate(index, 1);
            }
            var randomPos = spawnPoint.position + Random.insideUnitSphere * 30f;
            randomPos.y = 0;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 50f, 3))
            {
                go.transform.position = hit.position;
                go.SetActive(true);

                enemies.Add(go);
                disableEnemies.Remove(go);
            }
            yield return new WaitForSeconds(1.5f);
        }
    
    }

    private GameObject EnemyInstantiate(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(enemyPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
            go.GetComponent<Enemy>().SetUp(enemyDatas[index]);
            disableEnemies.Add(go);
        }

        return disableEnemies[index];
    }
    private void AddEnemyPrefabs()
    {
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.ZomBear));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Zombunny));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Hellephant));
    }

}
