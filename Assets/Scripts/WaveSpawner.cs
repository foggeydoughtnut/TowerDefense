using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;


    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;
    
    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update ()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }


    IEnumerator SpawnWave ()
    {

        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.enemies[i].count; j++)
            {
                SpawnEnemy(wave.enemies[i].enemy);
                yield return new WaitForSeconds(1f / wave.enemies[i].rate);
            }
            waveIndex++;
        }
        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            this.enabled = false;
        }
    }

    void SpawnEnemy (GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

}
