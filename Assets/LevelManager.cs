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
        
        SceneManager.sceneLoaded += HandleLoadedScene;
        EnemyController.OnDeath += HandleEnemyDeath;
        EnemyCounter.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;
    }

    private void HandleAllEnemiesDestroyed()
    {
        Debug.Log("All dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleLoadedScene(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name.Equals("DemoScene"))
        {
            spawner = FindObjectOfType<Spawner>();
        }
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
        EnemyCounter.OnAllEnemiesDestroyed -= HandleAllEnemiesDestroyed;
        SceneManager.sceneLoaded -= HandleLoadedScene;
        
    }

    private void HandleEnemyDeath(EnemyController obj)
    {
        
    }
}
