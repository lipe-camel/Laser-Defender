using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs; //The configurations and parameters within a wave, behaves like an Array[]

    int startingWave = 0;


    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllenemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllenemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 1; enemyCount <= waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
