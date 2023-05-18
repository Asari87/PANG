using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

using static UnityEngine.EventSystems.EventTrigger;

public enum EnemyType { Level1, Level2, Level3 }
public enum Direction { Left = -1, Right = 1 }

[CreateAssetMenu()]
public class EnemyDatabaseSO : ScriptableObject
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private List<SpawnHeirarchy> spawnList;
    public Dictionary<EnemyType, EnemyType> enemySpawnDict;

    public Dictionary<EnemyType, EnemySO> enemyStatsDictionary;
    public void Initialize()
    {
        enemyStatsDictionary = new();
        foreach (Enemy e in enemyList)
        {
            AddEnemyStats(e);
        }

        enemySpawnDict= new();
        foreach(SpawnHeirarchy spawn in spawnList)
        {
            enemySpawnDict.Add(spawn.parent, spawn.child);
        }


    }

    private void AddEnemyStats(Enemy e)
    {
        EnemySO newStats = ScriptableObject.CreateInstance<EnemySO>();
        newStats.type = e.type;

        newStats.axisForces = e.axisForces;
        newStats.enemyPrefab = e.enemyPrefab;

        newStats.popSound = e.popSound;
        newStats.popEffect = e.popEffect;

        newStats.bounceSound = e.bounceSound;
        newStats.bounceEffect = e.bounceEffect;

        enemyStatsDictionary.Add(e.type, newStats);
    }

    public bool HasNextSpawn(EnemyType type)
    {
        return enemySpawnDict.ContainsKey(type);
    }

    public EnemyType GetNextEnemySpawnType(EnemyType type)
    {
        return enemySpawnDict[type];
    }

    public EnemyController GetEnemyByType(EnemyType type, Vector3 position, Direction dir)
    {
        EnemySO stats = enemyStatsDictionary[type];
        float radius = stats.enemyPrefab.transform.localScale.x; ;
        EnemyController newEnemy = Instantiate(stats.enemyPrefab, position + ((int)dir * Vector3.right * radius/2), Quaternion.identity);
        newEnemy.SetStats(stats);
        newEnemy.SetDirection(dir);
        return newEnemy;
    }
}


[Serializable]
public class Enemy
{
    public EnemyType type;
    public Vector2 axisForces;
    
    public AudioClip bounceSound;
    public ParticleSystem bounceEffect;

    public AudioClip popSound;
    public ParticleSystem popEffect;

    public EnemyController enemyPrefab;

}

[Serializable]
public class SpawnHeirarchy
{
    public EnemyType parent;
    public EnemyType child;
}


