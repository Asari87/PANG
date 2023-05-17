using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public Vector2 axisForces;
    public AudioClip bounceSound;
    public AudioClip popSound;

    public GameObject enemyPrefab;
    public EnemySO childEnemyPrefab;


}
