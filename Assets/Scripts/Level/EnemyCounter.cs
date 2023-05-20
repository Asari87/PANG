using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private int enemyCounter = 0;
    public static Action OnAllEnemiesDestroyed;
    private void Awake()
    {
        EnemyController.OnPop += DecreaseCounter;
        EnemyController.OnSpawn += IncreaseCounter;
    }

    private void OnDestroy()
    {
        EnemyController.OnPop -= DecreaseCounter;
        EnemyController.OnSpawn -= IncreaseCounter;
    }

    private void IncreaseCounter(EnemyType type, Vector3 position)
    {
        enemyCounter++;
        Debug.Log($"Enemy counter: {enemyCounter}");
    }

    private void DecreaseCounter(EnemyType type, Vector3 position)
    {
        enemyCounter--;
        Debug.Log($"Enemy counter: {enemyCounter}");
        if(enemyCounter == 0)
            OnAllEnemiesDestroyed?.Invoke();
    }

}
