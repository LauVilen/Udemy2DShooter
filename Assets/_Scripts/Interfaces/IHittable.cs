using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHittable
{
    public UnityEvent OnGetHit { get; set; }
    public void GetHit(int dmg, GameObject dmgDealer);

}
