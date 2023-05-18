using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public EnemyType type;

    public Vector2 axisForces;
    public AudioClip bounceSound;
    public ParticleSystem bounceEffect;

    public AudioClip popSound;
    public ParticleSystem popEffect;

    public EnemyController enemyPrefab;
}
