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
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) return;
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
        var go = Instantiate(enemyToSpawnPrefab, posToSpawnAt, Quaternion.identity, transform);
        go.transform.rotation = new Quaternion(0, 0, 0, 0);
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

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < maxAmountOfEnemies; i++)
        {
            SpawnEnemy();
        }
        hasStarted = true;
    }
}
