using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public List<GameObject> skeletons;
    public GameObject skeletonSpawnPos;
    void Start()
    {
        skeletonPrefab = GameObject.Find("SkeletonPrefab");
        skeletonSpawnPos = GameObject.Find("SkeletonSpawnPos");
        skeletonPrefab.SetActive(false);
        skeletons = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        CheckDead();
    }

    void Spawn()
    {
        if(skeletons.Count > 1)
        {
            return;
        }
        GameObject skeleton = Instantiate(skeletonPrefab);
        skeleton.transform.position = skeletonSpawnPos.transform.position;
        skeleton.SetActive(true);
        skeletons.Add(skeleton);
    }
    void CheckDead()
    {
        foreach (GameObject enemy in skeletons)
        {
            if (enemy == null)
            {
                skeletons.Remove(enemy);
            }
        }
    }
}
