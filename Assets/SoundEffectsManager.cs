using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    private EnemyDatabaseSO database;
    private void Awake()
    {
        database = Resources.Load<EnemyDatabaseSO>("EnemyDB");
        EnemyController.OnDeath += HandleEnemyDeath;
        EnemyController.OnBounce += HandleEnemyBounce;
    }

    private void OnDestroy()
    {
        EnemyController.OnDeath += HandleEnemyDeath;
        EnemyController.OnBounce += HandleEnemyBounce;
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        EnemySO stats = database.GetEnemyStats(enemy.type);

        if (stats.popSound != null)
        {
            AudioSource.PlayClipAtPoint(stats.popSound, enemy.transform.position);
        }
        if (stats.popEffect != null)
        {
            Instantiate(stats.popEffect, enemy.transform.position, Quaternion.identity);
        }
    }

    private void HandleEnemyBounce(EnemyController enemy)
    {
        EnemySO stats = database.GetEnemyStats(enemy.type);
        //dispose of enemy
        if (stats.bounceSound != null)
        {
            AudioSource.PlayClipAtPoint(stats.bounceSound, enemy.transform.position);
        }
        if (stats.bounceEffect != null)
        {
            Instantiate(stats.bounceEffect, enemy.transform.position +(Vector3.down*enemy.transform.localScale.x/2), Quaternion.identity);
        }
    }

    
}
