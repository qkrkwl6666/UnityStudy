using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyData;

public class EnemysSpawner : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public Transform spawnPoint;

    private List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();

    public Dictionary<EnemyType, List<Enemy>> enemies = new Dictionary<EnemyType, List<Enemy>>();
    public Dictionary<EnemyType, List<Enemy>> disableEnemies = new Dictionary<EnemyType, List<Enemy>>();

    public int spawnCount { get; private set; } = 3;
    private void Awake()
    {
        InitializeEnemyPrefabs();
        InitializeEnemyDictionaries();
    }
    private void Start()
    {
        EnemyInstantiate((int)EnemyType.ZomBear, spawnCount);
        EnemyInstantiate((int)EnemyType.Zombunny, spawnCount);
        EnemyInstantiate((int)EnemyType.Hellephant, spawnCount);

        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (!PlayerInfo.isDead)
        {
            int randomNum = Random.Range(0, 101);
            EnemyType index = 0;

            if (randomNum <= enemyDatas[(int)EnemyType.ZomBear].percentage) index = EnemyType.ZomBear;
            else if (randomNum <= enemyDatas[(int)EnemyType.Zombunny].percentage) index = EnemyType.Zombunny;
            else if (randomNum <= enemyDatas[(int)EnemyType.Hellephant].percentage) index = EnemyType.Hellephant;

            if(disableEnemies[index] == null) continue;

            var go = disableEnemies[index].FirstOrDefault();

            if (!go)
            {
                go = Instantiate(enemyPrefabs[(int)index], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Enemy>();
                go.gameObject.SetActive(false);
                go.SetUp(enemyDatas[(int)index]);
            }
            else
            {
                disableEnemies[index].RemoveAt(0);
            }

            var randomPos = spawnPoint.position + Random.insideUnitSphere * 30f;
            randomPos.y = 0;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 50f, 3))
            {
                go.transform.position = hit.position;
                go.gameObject.SetActive(true);

                enemies[index].Add(go);
                go.OnEnemyDie += EnemiesDie;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void EnemyInstantiate(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(enemyPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
            go.SetActive(false);
            var enemy = go.GetComponent<Enemy>();
            enemy.SetUp(enemyDatas[index]);

            disableEnemies[(EnemyType)index].Add(enemy);

            enemy.OnEnemyDie += EnemiesDie;
        }
    }

    private void InitializeEnemyPrefabs()
    {
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.ZomBear));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Zombunny));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Hellephant));
    }

    private void InitializeEnemyDictionaries()
    {
        for(int i = 0; i < (int)EnemyData.EnemyType.Count; i++)
        {
            enemies.Add((EnemyType)i, new List<Enemy>());
            disableEnemies.Add((EnemyType)i, new List<Enemy>());
        }
    }

    private void EnemiesDie(Enemy enemy)
    {
        enemies[enemy.enemyType].Remove(enemy);
        disableEnemies[enemy.enemyType].Add(enemy);

        enemy.OnEnemyDie -= EnemiesDie;
    }

}
