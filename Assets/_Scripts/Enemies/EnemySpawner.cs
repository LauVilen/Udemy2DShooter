using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private List<GameObject> spawnPoints = null;
    [SerializeField] private int count = 20;
    [SerializeField] private float minDelay = 0.8f, maxDelay = 1.5f;

    [SerializeField] private List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();
    private float[] enemyWeights;

    IEnumerator SpawnCoroutine()
    {
        while (count>0)
        {
            count--;
            var randomIndex = Random.Range(0, spawnPoints.Count);
            var randomOffset = Random.insideUnitCircle;
            var spawnPoint = spawnPoints[randomIndex].transform.position + (Vector3)randomOffset;

            SpawnEnemy(spawnPoint);

            var randomTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        int index = GetRandomWeightedIndex(enemyWeights);
        Instantiate(enemiesToSpawn[index].enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void Start()
    {
        enemyWeights = enemiesToSpawn.Select(enemy => enemy.rate).ToArray();
        if (spawnPoints.Count>0)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint.transform.position);
            }
        }

        StartCoroutine(SpawnCoroutine());
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
}

[Serializable]
public struct EnemySpawnData
{
    [Range(0, 1)] public float rate;
    public GameObject enemyPrefab;
}
