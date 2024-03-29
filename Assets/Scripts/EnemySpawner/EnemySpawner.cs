using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnRate = 1f;
    
    private float _nextSpawnTime;

    [SerializeField, Range(0, 100)] private float spawnSphereRadius = 10f;
    

    private void Update()
    {
        if (!(Time.time >= _nextSpawnTime)) return;

        SpawnEnemy();
        _nextSpawnTime = Time.time + 1f / spawnRate;
    }

    private void SpawnEnemy()
    {
        // Generate a random angle between 0 and 360 degrees
        var angle = Random.Range(0f, 360f);

        // Convert the angle to radians
        angle *= Mathf.Deg2Rad;

        // Calculate the new spawn position
        var position = transform.position;
        var spawnPosition = new Vector3(
            position.x + spawnSphereRadius * Mathf.Cos(angle),
            position.y,
            position.z + spawnSphereRadius * Mathf.Sin(angle)
        );

        // Instantiate the enemy at the new spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Debugging
    [SerializeField] private bool debugSpawnSphere = false;
    private void OnDrawGizmos()
    {
        if (!debugSpawnSphere) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnSphereRadius);
    }
}