using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Transform[] spawnPoints; // spawns off screen
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

    // keeps track of spawned instances
    List<GameObject> activeEnemies = new List<GameObject>();

    // reference to GameManager for scaling health
    public GameManager gameManager;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // wait for next group
            yield return new WaitForSeconds(groupInterval);

            // NEW: Safety check so array never causes crashes
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogError("EnemySpawner ERROR: No spawn points assigned!");
                continue; // prevents a crash but keeps game running
            }

            // do not spawn if too many enemies present
            CleanActiveList();
            if (activeEnemies.Count >= maxSimultaneousEnemies)
                continue;

            // choose how many to spawn this group
            int toSpawn = Random.Range(groupMin, groupMax + 1);

            // spawn at a randomly chosen spawn point
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Slight vertical offsets to avoid stacking
            for (int i = 0; i < toSpawn; i++)
            {
                if (activeEnemies.Count >= maxSimultaneousEnemies) break;

                Vector3 pos = sp.position;
                pos.y += Random.Range(-1f, 1f) + i * 0.3f; // small spacing
                GameObject prefab = PickEnemyPrefabByProbability();
                if (prefab == null) continue;

                GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                SetupEnemy(go);

                activeEnemies.Add(go);

                // tiny delay between spawns in same group so they don't overlap at same frame
                yield return new WaitForSeconds(0.08f);
            }
        }
    }

    GameObject PickEnemyPrefabByProbability()
    {
        float roll = Random.value;
        if (roll < 0.6f) return standardPrefab; // 60%
        if (roll < 0.85f) return fastPrefab;    // 25%
        return fatPrefab;                       // 15%
    }

    void SetupEnemy(GameObject enemyGO)
    {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy == null) return;

        // set targetX to where barricade is
        enemy.targetX = gameManager != null ? gameManager.barricadeTargetX : 8f;

        // Scale hp and speed using GameManager difficulty multiplier
        enemy.maxHealth = Mathf.CeilToInt(enemy.maxHealth * gameManager.GetHealthMultiplier());
        enemy.moveSpeed *= gameManager.GetSpeedMultiplierForEnemy(enemy.enemyType);

        // register to notify when destroyed
        EnemyWatcher watcher = enemyGO.AddComponent<EnemyWatcher>();
        watcher.spawner = this;
    }

    // called by EnemyWatcher when an enemy is destroyed
    public void NotifyEnemyDestroyed(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);
    }

    void CleanActiveList()
    {
        activeEnemies.RemoveAll(x => x == null);
    }

    // helper class attached at runtime to watch destroy
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
