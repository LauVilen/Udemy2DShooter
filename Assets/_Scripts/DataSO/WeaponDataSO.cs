using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public BulletDataSO BulletData { get; set; }
    [SerializeField] private bool multiBulletShot = false;
    [SerializeField] [Range(1,10)] private int bulletCount = 1;

    [field: SerializeField]
    [field: Range(0, 100)]
    //field gets serialized in Unity. Range is set to between 0 and 100, which prevents the ammo value to be below 0.
    public int AmmoCapacity { get; set; } = 100; //Default value is 100

    [field: SerializeField] public bool AutomaticFire { get; internal set; } = false; //default value is false

    [field: SerializeField]
    [field: Range(0.1f, 2f)]
    public float WeaponDelay { get; internal set; } = 0.1f; //default value is 0.1 seconds

    [field: SerializeField]
    [field: Range(0, 5)]
    public float SpreadAngle { get; set; } = 5; //default is 5

    internal int GetBulletCountToSpawn()
    {
        if (multiBulletShot)
        {
            return bulletCount;
        }

        return 1;
    }
}
