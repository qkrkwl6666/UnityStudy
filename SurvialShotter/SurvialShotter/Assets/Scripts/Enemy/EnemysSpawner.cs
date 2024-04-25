using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemysSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<GameObject> disableEnemies = new List<GameObject>();
    private List<GameObject> enemyPrefabs = new List<GameObject>();

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
        while (true)
        {
            int index = Random.Range(0, disableEnemies.Count);

            var randomPos = spawnPoint.position + Random.insideUnitSphere * 30f;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 50f, 3))
            {
                disableEnemies[index].transform.position = hit.position;
                disableEnemies[index].SetActive(true);

                enemies.Add(disableEnemies[index]);
                disableEnemies.RemoveAt(index);

            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    private void EnemyInstantiate(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            disableEnemies.Add(Instantiate(enemyPrefabs[index], new Vector3(-10000, -10000, -10000), Quaternion.identity));
        }
    }

    private void AddEnemyPrefabs()
    {
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.ZomBear));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Zombunny));
        enemyPrefabs.Add(Resources.Load<GameObject>(Defines.Hellephant));
    }

}
