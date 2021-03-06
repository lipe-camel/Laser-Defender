﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs; //The configurations and parameters within a wave, behaves like an Array[]
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllenemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllenemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 1; enemyCount <= waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var enemy = Instantiate
                (waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); //to make the EnemyPathing script reckonize the waveConfig in SerializeField
            float randomTime = Random.Range(0, waveConfig.GetSpawnRandomFactor());
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns() + randomTime);
        }
    }
}
