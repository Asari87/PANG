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
        EnemyController.OnDeath += DecreaseCounter;
        EnemyController.OnSpawn += IncreaseCounter;
    }

    private void OnDestroy()
    {
        EnemyController.OnDeath -= DecreaseCounter;
        EnemyController.OnSpawn -= IncreaseCounter;
    }

    private void IncreaseCounter(EnemyController obj)
    {
        enemyCounter++;
        Debug.Log($"Enemy counter: {enemyCounter}");
    }

    private void DecreaseCounter(EnemyController obj)
    {
        enemyCounter--;
        Debug.Log($"Enemy counter: {enemyCounter}");
        if(enemyCounter == 0)
            OnAllEnemiesDestroyed?.Invoke();
    }

}
