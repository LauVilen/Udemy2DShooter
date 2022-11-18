using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public EnemySpawnDataSO spawnDataSO { get; set; }
    [SerializeField] private List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();
    [SerializeField] private List<GameObject> spawnPoints = null;
    private bool playerIsDead = false;
    private int count = 0;
    private float[] enemyWeights;

    private void Start()
    {
        count = spawnDataSO.amountToSpawn;
        enemyWeights = enemiesToSpawn.Select(enemy => enemy.rate).ToArray();
        if (spawnPoints.Count > 0)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint.transform.position);
            }
        }

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (count>0)
        {
            count--;
            var randomIndex = Random.Range(0, spawnPoints.Count);
            var randomOffset = Random.insideUnitCircle;
            var spawnPoint = spawnPoints[randomIndex].transform.position + (Vector3)randomOffset;

            SpawnEnemy(spawnPoint);

            var randomTime = Random.Range(spawnDataSO.minDelay, spawnDataSO.maxDelay);
            yield return new WaitForSeconds(randomTime);
            if (playerIsDead)
            {
                break;
            }
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        int index = GetRandomWeightedIndex(enemyWeights);
        Instantiate(enemiesToSpawn[index].enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private int GetRandomWeightedIndex(float[] enemyWeights)
    {
        float sum = 0f;
        for (int i = 0; i < enemyWeights.Length; i++)
        {
            sum += enemyWeights[i];
        }
        float randomValue = Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < enemyWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + enemyWeights[i])
            {
                return i;
            }

            tempSum += enemyWeights[i];
        }
        return 0;
    }

    public void StopSpawning()
    {
        playerIsDead = true;
    }
}

[Serializable]
public struct EnemySpawnData
{
    [Range(0, 1)] public float rate;
    public GameObject enemyPrefab;
}
