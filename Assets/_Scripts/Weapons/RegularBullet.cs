using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rb;

    public override BulletDataSO BulletData
    {
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rb = GetComponent<Rigidbody2D>();
            rb.drag = BulletData.Friction;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && BulletData != null)
        {
            //rb uses MovePosition instead of Velocity, since it is Kinematic instead of Dynamic
            rb.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(bulletData.Damage, gameObject);
        //checks if the bullet hits the wall or other obstacles and calls the appropriate function
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            HitObstacle();
        }
        //checks if the bullet hits an enemy and calls the appropriate function
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            HitEnemy();
        }
        //lastly the game object (bullet) is destroyed, so it does not exist eternally in the hierarchy and slows the program down
        Destroy(gameObject);
    }

    private void HitObstacle()
    {
        Debug.Log("Hitting Obstacle");
    }

    private void HitEnemy()
    {
        Debug.Log("Hitting Enemy");
    }
}