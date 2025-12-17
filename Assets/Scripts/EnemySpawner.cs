using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Transform[] spawnPoints;
    public GameObject standardPrefab;
    public GameObject fastPrefab;
    public GameObject fatPrefab;

    [Tooltip("Seconds between group spawns")]
    public float groupInterval = 2.0f;
    [Tooltip("How many enemies in a single spawned group (random between min & max)")]
    public int groupMin = 1;
    public int groupMax = 3;

    [Header("Instance Limits")]
    public int maxSimultaneousEnemies = 20;

    List<GameObject> activeEnemies = new List<GameObject>();

    public GameManager gameManager;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(groupInterval);

            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogError("EnemySpawner ERROR: No spawn points assigned!");
                continue;
            }

            CleanActiveList();

            if (activeEnemies.Count >= maxSimultaneousEnemies)
                continue;

            int toSpawn = Random.Range(groupMin, groupMax + 1);
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            for (int i = 0; i < toSpawn; i++)
            {
                if (activeEnemies.Count >= maxSimultaneousEnemies) break;

                Vector3 pos = sp.position;
                pos.y += Random.Range(-1f, 1f) + i * 0.3f;

                GameObject prefab = PickEnemyPrefabByProbability();
                if (prefab == null) continue;

                GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                SetupEnemy(go);

                activeEnemies.Add(go);

                yield return new WaitForSeconds(0.08f);
            }
        }
    }

    GameObject PickEnemyPrefabByProbability()
    {
        float roll = Random.value;
        if (roll < 0.6f) return standardPrefab;
        if (roll < 0.85f) return fastPrefab;
        return fatPrefab;
    }

    void SetupEnemy(GameObject enemyGO)
    {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy == null) return;

        // Set target X to barricade
        if (gameManager != null)
            enemy.targetX = gameManager.barricadeTargetX;

        // Use existing values w/out scaling

        EnemyWatcher watcher = enemyGO.AddComponent<EnemyWatcher>();
        watcher.spawner = this;
    }

    public void NotifyEnemyDestroyed(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);
    }

    void CleanActiveList()
    {
        activeEnemies.RemoveAll(x => x == null);
    }

    class EnemyWatcher : MonoBehaviour
    {
        public EnemySpawner spawner;
        void OnDestroy()
        {
            if (spawner != null)
                spawner.NotifyEnemyDestroyed(gameObject);
        }
    }
}
