using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private bool dead = false;
    [field: SerializeField] public int Health { get; private set; } = 6;
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDie { get; set; }

    public void GetHit(int dmg, GameObject dmgDealer)
    {
        if (dead == false)
        {
            Health-=dmg;
            Debug.Log("Player hit");
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                OnDie?.Invoke();
                dead = true;
                StartCoroutine(DeathCoroutine());
            }
        }
    }

    private IEnumerator DeathCoroutine()
    {
        //pauses the "thread" before carrying out any further logic
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
