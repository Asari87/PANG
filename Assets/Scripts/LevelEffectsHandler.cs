using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelEffectsHandler : MonoBehaviour
{
    private EnemyDatabaseSO database;
    private void Awake()
    {
        database = Resources.Load<EnemyDatabaseSO>("EnemyDB");
        EnemyController.OnPop += HandleEnemyDeath;
        EnemyController.OnBounce += HandleEnemyBounce;
    }

    private void OnDestroy()
    {
        EnemyController.OnPop += HandleEnemyDeath;
        EnemyController.OnBounce += HandleEnemyBounce;
    }

    private void HandleEnemyDeath(EnemyType type, Vector3 position)
    {
        EnemySO stats = database.GetEnemyStats(type);

        if (stats.popSound != null && stats.popSound.Length > 0)
        {
            AudioClip randomAudio = stats.popSound[Random.Range(0, stats.popSound.Length)];
            SoundManager.Instance.PlayClipAtPoint(randomAudio, position);
        }
        if (stats.popEffect != null && stats.popEffect.Length > 0)
        {
            ParticleSystem randomEffect = stats.popEffect[Random.Range(0,stats.popEffect.Length)];
            Instantiate(randomEffect, position, Quaternion.identity);
        }
    }

    private void HandleEnemyBounce(EnemyType type, Vector3 position)
    {
        EnemySO stats = database.GetEnemyStats(type);
        //dispose of enemy
        if (stats.bounceSound != null && stats.bounceSound.Length > 0 )
        {
            AudioClip randomAudio = stats.bounceSound[Random.Range(0, stats.bounceSound.Length)];
            SoundManager.Instance.PlayClipAtPoint(randomAudio, position);
        }
        if (stats.bounceEffect != null && stats.bounceEffect.Length > 0)
        {
            ParticleSystem randomEffect = stats.bounceEffect[Random.Range(0, stats.popEffect.Length)];
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, Mathf.Infinity, LayerMask.NameToLayer("Ground"));
            if(hit.transform != null)
                Instantiate(randomEffect, hit.point, Quaternion.identity);
        }
    }

    
}
