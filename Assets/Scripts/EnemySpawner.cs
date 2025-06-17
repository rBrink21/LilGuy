using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [Tooltip("Time in seconds between spawns")]
    [SerializeField] private float spawnDelay;
    [Tooltip("How many enemies currently alive before stopping spawning, will spawn a new enemy after one dies. Set to -1 to enforce no limit (not recommended unless you have a very long delay)")]
    [SerializeField] private int maxEnemiesAlive;
    [Tooltip("How many enemies it will spawn before it stops spawning all together. Set to -1 to enforce no limit")]
    [SerializeField] private int maxEnemiesSpawned;

    private int currentAlive;
    private int spawned;
    private float timeSinceLastSpawned = Mathf.Infinity;

    private void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if (CanSpawn())
        {
            SpawnEnemy();
        }
    }

    private bool CanSpawn()
    {
        if (timeSinceLastSpawned < spawnDelay)
        {
            return false;
        }

        if (maxEnemiesSpawned > -1)
        {
            if (spawned >= maxEnemiesSpawned)
            {
                return false;
            }
        }

        if (maxEnemiesAlive > -1)
        {
            if (currentAlive >= maxEnemiesAlive)
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnEnemy()
    {
        if (!enemiesToSpawn.Any())
        {
            Debug.LogWarning("Enemy spawner is without enemies to spawn!");
        }
        GameObject enemy = enemiesToSpawn[GetIndex()];
        var enemyInstance = Instantiate(enemy);
        enemyInstance.transform.position = transform.position;
        timeSinceLastSpawned = 0;
        spawned++;
        currentAlive++;

        Health enemyHealth = enemyInstance.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.hasDied += () =>  currentAlive--;
        }
    }

    private int GetIndex()
    {
        return spawned % enemiesToSpawn.Count();
    }
}
