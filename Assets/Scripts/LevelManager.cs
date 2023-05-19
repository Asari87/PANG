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
        EnemyCounter.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;

        SceneManager.sceneLoaded += HandleNewScene;
    }

    private void HandleNewScene(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Contains("Level"))
            spawner = FindFirstObjectByType<Spawner>();
    }

    private void HandleAllEnemiesDestroyed()
    {
        Debug.Log("All dead");
        StartCoroutine(EndLevelRoutine());
    }

    private IEnumerator EndLevelRoutine()
    {
        yield return new WaitForSeconds(3);
        SceneHandler.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

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
        SceneManager.sceneLoaded -= HandleNewScene;

    }


}
