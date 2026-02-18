using System.Collections;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entity;
    public float minSpawnDistance, maxSpawnDistance, spawnDelay;
    private Transform playerTransform;
    public void Start()
    {
        playerTransform = transform;
        StartCoroutine(SpawnEntity(entity, minSpawnDistance, maxSpawnDistance, spawnDelay));
    }

    private IEnumerator SpawnEntity(GameObject entityToSpawn, float spawnMinDistance, float spawnMaxDistance, float delay)
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        while (!validPositionFound)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(spawnMinDistance, spawnMaxDistance);
            Vector3 potentialPosition = playerTransform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * randomDistance;

            if (Vector3.Distance(potentialPosition, playerTransform.position) >= spawnMinDistance)
            {
                spawnPosition = potentialPosition;
                validPositionFound = true;
            }

        }
        Instantiate(entityToSpawn, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnEntity(entityToSpawn, spawnMinDistance, spawnMaxDistance, delay));
    }

    public void UpgradeBeast()
    {
        Beast upgrade = entity.GetComponent<Beast>();
        upgrade.Upgrade();
    }
}
