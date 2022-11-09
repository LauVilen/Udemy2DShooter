using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private bool dead = false;
    private PlayerWeapon playerWeapon = null;
    [field: SerializeField] private int maxHealth;
    private int health;
    public int Health {
        get => health;
        set
        {
            health = Mathf.Clamp(value,0,maxHealth);
            UiHealth.UpdateUI(health);
        }

    }
    [field: SerializeField] public UIHealth UiHealth { get; set; } 
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDie { get; set; }

    private void Start()
    {
        Health = maxHealth;
        UiHealth.Initialize(Health);
    }

    private void Awake()
    {
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            var resource = collision.gameObject.GetComponent<Resource>();
            if (resource!=null)
            {
                switch (resource.ResourceData.ResourceType)
                {
                    case ResourceEnum.Health:
                        if (Health>=maxHealth)
                        {
                            return;
                        }
                        else
                        {
                            Health += resource.ResourceData.GetAmount();
                            resource.PickUpResource();
                            Debug.Log(Health);
                        }
                        break;
                    case ResourceEnum.Ammo:
                        if (playerWeapon.AmmoFull)
                        {
                            return;
                        }
                        else
                        {
                            playerWeapon.AddAmmo(resource.ResourceData.GetAmount());
                            resource.PickUpResource();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
