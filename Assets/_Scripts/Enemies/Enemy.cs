using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    private bool dead = false;
    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public int Health { get; private set; } = 2; //default value
    [field: SerializeField] public EnemyAttack EnemyAttack { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDie { get; set; }
    public void GetHit(int dmg, GameObject dmgDealer)
    {
        if (dead == false)
        {
            //decreased health
            Health-= dmg;
            //invokes the unity event, in case something is listening to it
            OnGetHit?.Invoke();
            //Destroy the enemy if its health is 0 or less
            if (Health <= 0)
            {
                dead = true;
                OnDie?.Invoke();
                StartCoroutine(WaitToDie());
            }
        }
    }

    //coroutine for the enemy-death mechanic
    IEnumerator WaitToDie()
    {
        //pauses the "thread" before carrying out any further logic
        yield return new WaitForSeconds(.54f);
        Destroy(gameObject);
    }

    private void Awake()
    {
        if (EnemyAttack == null)
        {
            EnemyAttack = GetComponent<EnemyAttack>();
        }
    }

    private void Start()
    {
        Health = EnemyData.MaxHealth;
    }

    public void PerformAttack()
    {
        if (dead == false)
        {
            EnemyAttack.Attack(EnemyData.Damage);
        }
    }
}
