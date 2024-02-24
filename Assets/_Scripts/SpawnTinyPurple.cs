using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTinyPurple : SpawnObject
{
    protected override void Awake()
    {
        namePrefab = "TinyPurplePrefab";
        nameSpawnObjectOnMap = "SpawnTinyPurpleOnMap";
        base.Awake();
    }
}
