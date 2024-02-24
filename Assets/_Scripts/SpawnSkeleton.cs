using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : SpawnObject
{
    protected override void Awake()
    {
        namePrefab = "SkeletonPrefab";
        nameSpawnObjectOnMap = "SpawnSkeletonOnMap";
        base.Awake();
    }
}
