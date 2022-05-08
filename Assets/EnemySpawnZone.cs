using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    public GameObject enemyToSpawnPrefab;
    public int maxAmountOfEnemies = 3;
    public float spawnTime = 10f;
    public Vector2 boundries;

    bool isCurrentlySpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxAmountOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckToRespawnEnemy();
    }

    private void CheckToRespawnEnemy()
    {
        if (transform.childCount < maxAmountOfEnemies)
        {
            if (!isCurrentlySpawning)
            {
                StartCoroutine(SpawnEnemyInSecs(spawnTime));
            }
        }
    }

    void SpawnEnemy()
    {
        var posToSpawnAt = transform.position + new Vector3(
            (Random.value - 0.5f) * boundries.x,
            (Random.value - 0.5f) * boundries.y
            );
        Instantiate(enemyToSpawnPrefab, posToSpawnAt, Quaternion.identity, transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundries.x, boundries.y));
    }

    IEnumerator SpawnEnemyInSecs(float secondsToSpawn)
    {
        isCurrentlySpawning = true;
        yield return new WaitForSeconds(secondsToSpawn);
        SpawnEnemy();
        isCurrentlySpawning = false;
    }
}
