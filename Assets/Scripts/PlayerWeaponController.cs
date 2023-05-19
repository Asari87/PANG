using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponStats;
    [SerializeField] private Transform shootingPoint;
    
    private float timeSinceLastShot = float.MaxValue;


    private bool canShoot = false;
    
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        canShoot = timeSinceLastShot > weaponStats.fireRate;
    }

    public void Shoot()
    {
        if (!canShoot) return;
        BulletBehaviour bullet = Instantiate(weaponStats.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.SetSpeed(weaponStats.fireForce);

        if(weaponStats.fireSounds.Length> 0 )
        {
            AudioClip randomClip = weaponStats.fireSounds[UnityEngine.Random.Range(0, weaponStats.fireSounds.Length)];
            SoundManager.Instance.PlayClipAtPoint(randomClip, Camera.main.transform.position);
        }
        if(weaponStats.fireEffect != null)
        {
            Instantiate(weaponStats.fireEffect,shootingPoint.position, shootingPoint.rotation);
        }
        timeSinceLastShot = 0;
    }


}
