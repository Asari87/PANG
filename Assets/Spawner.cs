using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnDetails> spawnDetails = new List<SpawnDetails>();
}

[Serializable]
public class SpawnDetails
{
    public Transform startingPoint;
    public EnemyType type;
}

