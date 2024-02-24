using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : SpawnObject
{
    protected override void Awake()
    {
        namePrefab = "SlimePrefab";
        nameSpawnObjectOnMap = "SpawnSlimeOnMap";
        base.Awake();
    }
}