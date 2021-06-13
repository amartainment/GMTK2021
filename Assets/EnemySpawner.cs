using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> spawnPoints;
    public Text waveText;
    Transform chosenSpawnPoint;
    public int enemiesPerWave = 10;
    int deadEnemies = 0;
    public int waves = 3;
    int currentWave = 0;
    public GameObject enemyPrefab;
    public GameObject winText;
    

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
        AnimateWaveText();
    }

    // Update is called once per frame
    void Update()
    {
        if(winText.gameObject.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void enemyDeath(int i)
    {
        deadEnemies++;
        if(deadEnemies == enemiesPerWave)
        {
            deadEnemies = 0;
            StartCoroutine("SpawnEnemyTimer");
            AnimateWaveText();
        }
    }

    void AnimateWaveText()
    {
        string newText = "W A V E  " + currentWave.ToString();
        waveText.text = newText;
        waveText.GetComponent<Animator>().Play("flickertext");
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
            if (currentWave != waves)
            {
                while (enemiesCreated < enemiesPerWave)
                {
                    Instantiate(enemyPrefab, returnSpawnPoint().position, Quaternion.identity);
                    enemiesCreated++;
                    yield return new WaitForSeconds(4);

                }

                
            } else
            {
                while (enemiesCreated < enemiesPerWave)
                {
                    Instantiate(enemyPrefab, returnSpawnPoint().position, Quaternion.identity);
                    enemiesCreated++;
                    HideSpawnPoints();
                }
                

            }
        } else
        {
            winText.SetActive(true);
        }
    }

    void HideSpawnPoints()
    {
foreach(Transform t in spawnPoints)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
