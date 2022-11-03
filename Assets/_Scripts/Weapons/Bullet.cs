using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    // [field: SerializeField]
    // public virtual BulletDataSO BulletData { get; set; }

    [SerializeField] protected BulletDataSO bulletData;
    public virtual BulletDataSO BulletData
    {
        get { return bulletData; }
        set { bulletData = value; }
    }
}
