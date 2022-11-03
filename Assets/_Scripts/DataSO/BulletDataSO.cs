using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/BulletData")]
public class BulletDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject BulletPrefab { get; set; }

    [field: SerializeField]
    [field: Range(1, 100)]
    public float BulletSpeed { get; internal set; } = 1; //default speed is 1

    [field: SerializeField]
    [field: Range(1, 10)]
    public int Damage { get; set; } = 1; //default value is 1

    [field: SerializeField]
    [field: Range(0, 10)]
    public float Friction { get; set; } = 0; //default value

    //field to determine whether bullets can bounce off of walls and obstacles
    [field: SerializeField] public bool Bounce { get; set; } = false; //default value

    //field to determine whether bullets can go through enemies to damage more than one target
    [field: SerializeField] public bool GoThroughHittable { get; set; } = false; //default value

    [field: SerializeField] public bool IsRayCast { get; set; } = false; //default value

    [field: SerializeField] public GameObject ImpactObstaclePrefab { get; set; }
    [field: SerializeField] public GameObject ImpactEnemyPrefab { get; set; }

    [field: SerializeField]
    [field: Range(1, 20)]
    public float KnockBackPower { get; set; } = 5; //default value

    [field: SerializeField]
    [field: Range(0.01f, 1f)]
    public float KnockBackDelay { get; set; } = 0.1f; //default value
}
