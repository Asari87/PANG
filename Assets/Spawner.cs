using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnDetails> spawnDetails = new List<SpawnDetails>();
    private EnemyDatabaseSO database;
    private void Awake()
    {
        EnemyController.OnDeath += HandleEnemyDeath;
        database = Resources.Load<EnemyDatabaseSO>("EnemyDB");
    }
    private void StartLevel()
    {
        foreach (SpawnDetails sd in spawnDetails)
        {
            database.GetEnemyByType(sd.type, sd.startingPoint.position, Direction.Right);
        }
    }
    private void OnDestroy()
    {
        EnemyController.OnDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyType type, Vector3 position)
    {
        //Handle child spawns first
        if (database.HasNextSpawn(type))
        {
            EnemyType nextType = database.GetNextEnemySpawnType(type);
            database.GetEnemyByType(nextType, position, Direction.Left);
            database.GetEnemyByType(nextType, position, Direction.Right);
        }
    }
}

[Serializable]
public class SpawnDetails
{
    public Transform startingPoint;
    public EnemyType type;
}

