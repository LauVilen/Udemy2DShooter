using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemySpawnData")]
public class EnemySpawnDataSO : ScriptableObject
{
    [field: SerializeField] public int amountToSpawn;
    [field: SerializeField] public float minDelay;
    [field: SerializeField] public float maxDelay;
}
