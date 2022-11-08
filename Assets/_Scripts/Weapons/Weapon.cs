using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    #region Props and fields

    [SerializeField] protected GameObject muzzle;

    //amount of ammo in weaopon
    [SerializeField] protected int ammo = 10;

    //creates a field in unity, to link the Weapon data to the current script, and creates an instance through which the values of the class can be read
    [SerializeField] protected WeaponDataSO weaponData;

    /// <summary>
    /// Gets and sets the value of ammo in the weapon
    /// </summary>
    public int Ammo
    {
        get { return ammo; }
        //set method makes sure ammo is never below 0 and cannot exeed the ammocapacity (ammo capacity set in WeaponDataSO)
        set
        {
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
            OnAmmoChange?.Invoke(ammo);
        }
    }

    public bool AmmoFull { get => Ammo >=weaponData.AmmoCapacity; }

    protected bool isShooting = false;
    [SerializeField] protected bool reloadCoroutine = false; //Serialize in case of debug

    //Event used to play shot audio
    [field:SerializeField] public UnityEvent OnShoot { get; set; }

    //Event used to play shot audio, when out of bullets
    [field: SerializeField] public UnityEvent OnShootNoAmmo { get; set; }

    [field: SerializeField] public UnityEvent<int> OnAmmoChange { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
    }

    private void Update()
    {
        UseWeapon();
    }

    //Renamed from "Shoot" as we might get NoAmmo event when weapon is out of ammo
    public void TryShooting()
    {
        //Set the shooting flag
        isShooting = true;
    }

    public void StopShooting()
    {
        //Set the shooting flag
        isShooting = false;
    }

    /// <summary>
    /// Adds ammo to the weapon by the specified amount
    /// </summary>
    /// <param name="ammo"></param>
    public void Reload(int ammo)
    {
        Ammo += ammo;
    }
    private void UseWeapon()
    {
        //checks that methods to shoot the weapons has been called and that the weapon is not currently loading
        if (isShooting && reloadCoroutine == false)
        {
            if (Ammo > 0)
            {
                Ammo--;
                //invokes the unity event, which plays the shot sound
                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                //invokes the unity event, which plays the out-of-ammo sound
                OnShootNoAmmo?.Invoke();
                return;
            }

            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShotCoroutine());
        //check if the weapon is automatic or semi-automatic
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    //Method that creates a delay between shots
    protected IEnumerator DelayNextShotCoroutine()
    {
        reloadCoroutine = true;
        //"pauses" the execution of the next statement by a number of seconds, which is defined in the weapondata
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        //Here the GetComponent refers to the abstract class "Bullet", which means that child classes can also be assigned.
        //Thus the SpawnBullet method is universal to different bullet types
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    //A Quaternion represents the rotation
    private Quaternion CalculateAngle(GameObject muzzle)
    {
        //Use UnityEngine.Random
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0,0,spread));

        //to add to a Quaternion you have to multiply
        return muzzle.transform.rotation * bulletSpreadRotation;
    }

    #endregion


}
