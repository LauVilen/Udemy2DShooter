using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Items/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    [field:SerializeField]
    public ResourceEnum ResourceType { get; set; }

    [SerializeField] private int minAmount = 1;
    [SerializeField] private int maxAmount = 5;

    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount+1);
    }
}

public enum ResourceEnum
{
    None,
    Health,
    Ammo
}
