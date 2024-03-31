using System.Collections.Generic;
using Health;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Spawner Settings")]
        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField, Range(0, 100)] private float spawnSphereRadius = 10f;
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private int difficulty = 1;
        
        private float _nextSpawnTime;
        
        [Header("Wave Settings")]
        [SerializeField] private int currentWave = 1;
        
        [SerializeField] private int enemiesPerWave = 10;
        [SerializeField] private int enemiesThisWave = 10;
        
        [SerializeField] private int enemiesSpawned = 0;

        [SerializeField] private int enemiesKilled = 0;
        [SerializeField] private int enemiesKilledThisWave = 0;
        
        [SerializeField] private List<GameObject> spawnedEnemies;
        
        public Camera playerCamera;
        public Transform playerTransform;
        public delegate void OnEnemySpawnedAction(GameObject enemy);
        public event OnEnemySpawnedAction OnEnemySpawned;
        
        private void Update()
        {
            if (!(Time.time >= _nextSpawnTime)) return;

            SpawnEnemy();
            _nextSpawnTime = Time.time + 1f / spawnRate;

        }
        
        private void SpawnEnemy()
        {

            for (var i = enemyPrefabs.Length - 1; i >= 0; i--)
            {
                while (i <= difficulty && enemiesSpawned < enemiesPerWave)
                {
                    SpawnEnemyOfType(i);
                    difficulty -= i;
                    enemiesSpawned++;
                }
            }

            while (enemiesSpawned < enemiesPerWave)
            {
                SpawnEnemyOfType(0);
                enemiesSpawned++;
            }

            if (enemiesThisWave > enemiesKilledThisWave) return;
            
            currentWave++;
            difficulty = currentWave;
            
            enemiesSpawned = 0;
            enemiesKilledThisWave = 0;
            
            enemiesPerWave += 5;
            enemiesThisWave = enemiesPerWave;
        }

        private void SpawnEnemyOfType(int index)
        {
            Vector3 spawnPosition;
            var position = transform.position;

            do
            {
                var angle = Random.Range(0f, 360f);
                angle *= Mathf.Deg2Rad;

                spawnPosition = new Vector3(
                    position.x + spawnSphereRadius * Mathf.Cos(angle),
                    position.y,
                    position.z + spawnSphereRadius * Mathf.Sin(angle)
                );

            } while (IsPositionInCameraFrustum(playerCamera, spawnPosition));

            var enemyPrefab = enemyPrefabs[index];
            var enemyMovement = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<EnemyMovement>();
            
            spawnedEnemies.Add(enemyMovement.gameObject);
            OnEnemySpawned?.Invoke(enemyMovement.gameObject);
            
            enemyMovement.GetComponent<EnemyHealth>().OnDie += OnEnemyDeath;
            
            enemyMovement.Target = playerTransform.transform;
        }

        private void OnEnemyDeath(GameObject enemy)
        {
            spawnedEnemies.Remove(enemy);
            enemiesKilled++;
            enemiesKilledThisWave++;
        }
        
        private bool IsPositionInCameraFrustum(Camera cam, Vector3 position)
        {
            var bounds = new Bounds(position, Vector3.zero);
            var planes = GeometryUtility.CalculateFrustumPlanes(cam);
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }

        [SerializeField] private bool debugSpawnSphere = false;

        private void OnDrawGizmos()
        {
            if (!debugSpawnSphere) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnSphereRadius);
        }
    }
}