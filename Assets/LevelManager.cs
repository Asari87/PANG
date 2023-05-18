using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.EventSystems.EventTrigger;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemyDatabaseSO database;
    [SerializeField] private Spawner spawner;
    private void Awake()
    {
        database.Initialize();
        EnemyController.OnDeath += HandleEnemyDeath;
    }
    private void Start()
    {
        foreach(SpawnDetails sd in spawner.spawnDetails)
        {
            database.GetEnemyByType(sd.type, sd.startingPoint.position, Direction.Right);
        }
    }
    private void OnDestroy()
    {
        EnemyController.OnDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        if(database.HasNextSpawn(enemy.enemyStats.type))
        {
            EnemyType nextType = database.GetNextEnemySpawnType(enemy.enemyStats.type);
            database.GetEnemyByType(nextType, enemy.transform.position, Direction.Left);
            database.GetEnemyByType(nextType, enemy.transform.position, Direction.Right);
        }
        
        Destroy(enemy.gameObject);
    }


}
