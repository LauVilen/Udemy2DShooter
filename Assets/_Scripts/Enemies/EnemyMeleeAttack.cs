using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int dmg)
    {
        if (waitBeforeNextAttack == false)
        {
            var hittable = GetTarget().GetComponent<IHittable>();
            hittable?.GetHit(dmg, gameObject);
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
}
