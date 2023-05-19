using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static UnityEngine.EventSystems.EventTrigger;

public class LevelManager : MonoBehaviour
{
    private EnemyDatabaseSO database;
    private Spawner spawner;
    private void Awake()
    {
        database = Resources.Load<EnemyDatabaseSO>("EnemyDB");
        database.Initialize();
        spawner = GetComponentInChildren<Spawner>();
        EnemyCounter.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;
    }

    private void HandleAllEnemiesDestroyed()
    {
        Debug.Log("All dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        EnemyCounter.OnAllEnemiesDestroyed -= HandleAllEnemiesDestroyed;
        
    }


}
