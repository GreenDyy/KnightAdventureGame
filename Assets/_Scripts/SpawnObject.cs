using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public List<GameObject> objs;
    public GameObject objPrefab;
    public List<Transform> spawnPositions;
    public string namePrefab;
    public string nameSpawnObjectOnMap;
    protected virtual void Awake()
    {
        objs = new List<GameObject>();
        spawnPositions = new List<Transform>();
        objPrefab = GameObject.Find(namePrefab);
        objPrefab.SetActive(false);
    }
    private void Start()
    {
        LoadSpawnPositions();
        Spawn();
    }

    private void Update()
    {
        CheckDead();
    }

    private void LoadSpawnPositions()
    {
        Transform spawnPointsContainer = GameObject.Find(nameSpawnObjectOnMap).transform;
        foreach (Transform spawnPoint in spawnPointsContainer)
        {
            spawnPositions.Add(spawnPoint);
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Transform spawnPos = spawnPositions[i];

            GameObject obj = Instantiate(objPrefab);
            obj.transform.position = spawnPos.position;
            obj.SetActive(true);
            objs.Add(obj);
            obj.transform.parent = transform;
        }
    }

    void CheckDead()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i] == null)
                objs.RemoveAt(i);
        }
    }
}
