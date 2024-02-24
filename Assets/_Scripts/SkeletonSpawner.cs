using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject skeletonSpawnPos;
    public List<GameObject> enemies;
    public int maxEnemy = 2;

    public float timer = 0f;
    public float delay = 2f;

    private void Awake()
    {
        skeletonPrefab = GameObject.Find("SkeletonPrefab");
        skeletonSpawnPos = GameObject.Find("SkeletonSpawnPos");
        skeletonPrefab.SetActive(false);
        enemies = new List<GameObject>();
    }
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        Spawn();
    }

    void Spawn()
    {
        timer += Time.deltaTime;

        if (timer < delay)
        {
            return;
        }
        timer = 0f;

        if (enemies.Count < maxEnemy)
        {
            GameObject enemy = Instantiate(skeletonPrefab);
            //Vector3 newSpawnPos = skeletonSpawnPos.transform.position;
            //newSpawnPos.y = 0.0f;
            enemy.transform.position = skeletonSpawnPos.transform.position;
            enemy.SetActive(true);
            enemies.Add(enemy);
            enemy.transform.parent = transform;
        }
    }

    void CheckDead()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }
}
