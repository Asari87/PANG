using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Laser}

[CreateAssetMenu(fileName = "New Weapon", menuName = "PANG/Weapon/New Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private WeaponType weapon;

    [Header("Bullet properties")]
    public BulletBehaviour bulletPrefab;
    [Tooltip("Sets the speed of newly created butllet")] 
    public float fireForce;
    [Tooltip("This sets the interval between shots")] 
    public float fireRate;

    [Header("Effects")]
    public ParticleSystem fireEffect;
    public AudioClip[] fireSounds;


}
