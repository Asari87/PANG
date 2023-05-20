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
    private static LevelManager Instance;
    [SerializeField] private GameObject levelCompletePanel;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        levelCompletePanel.SetActive(false);
        database = Resources.Load<EnemyDatabaseSO>("EnemyDB");
        database.Initialize();
        EnemyCounter.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;

        SceneManager.sceneLoaded += HandleNewScene;
    }

    private void HandleNewScene(Scene scene, LoadSceneMode mode)
    {
        levelCompletePanel.SetActive(false);
        if (scene.name.Contains("Level"))
        {
            spawner = FindFirstObjectByType<Spawner>();
            foreach (SpawnDetails sd in spawner.spawnDetails)
            {
                database.GetEnemyByType(sd.type, sd.startingPoint.position, Direction.Right);
            }
        }
        else
            Destroy(gameObject);
    }

    private void HandleAllEnemiesDestroyed()
    {
        Debug.Log("All dead");
        StartCoroutine(EndLevelRoutine());
    }

    private IEnumerator EndLevelRoutine()
    {
        yield return new WaitForSeconds(.5f);
        levelCompletePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneHandler.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void OnDestroy()
    {
        EnemyCounter.OnAllEnemiesDestroyed -= HandleAllEnemiesDestroyed;
        SceneManager.sceneLoaded -= HandleNewScene;

    }


}
