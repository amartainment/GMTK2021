using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> spawnPoints;
    Transform chosenSpawnPoint;
    public int enemiesPerWave = 10;
    int deadEnemies = 0;
    public int waves = 3;
    int currentWave = 0;
    public GameObject enemyPrefab;

    private void OnEnable()
    {
        MyEventSystem.enemyDead += enemyDeath;
    }

    private void OnDisable()
    {
        MyEventSystem.enemyDead -= enemyDeath;
    }



    void Start()
    {
        StartCoroutine("SpawnEnemyTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enemyDeath(int i)
    {
        deadEnemies++;
        if(deadEnemies == enemiesPerWave)
        {
            deadEnemies = 0;
            StartCoroutine("SpawnEnemyTimer");
        }
    }

    Transform returnSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count-1);
        return spawnPoints[randomIndex];
    }

    IEnumerator SpawnEnemyTimer()
    {
        if (currentWave < waves)
        {
            currentWave++;
            Debug.Log("wave started");
            yield return new WaitForSeconds(5);
            int enemiesCreated = 0;
            while (enemiesCreated < enemiesPerWave)
            {
                Instantiate(enemyPrefab, returnSpawnPoint().position, Quaternion.identity);
                enemiesCreated++;
                yield return new WaitForSeconds(4);

            }
        } else
        {
            yield return null;
        }
    }
}
