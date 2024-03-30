using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyPrefab;

        [SerializeField] private float spawnRate = 1f;

        private float _nextSpawnTime;

        [SerializeField, Range(0, 100)] private float spawnSphereRadius = 10f;

        public Camera playerCamera;
        public Transform playerTransform;

        private void Update()
        {
            if (!(Time.time >= _nextSpawnTime)) return;
    
            SpawnEnemy();
            _nextSpawnTime = Time.time + 1f / spawnRate;
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPosition; 
            var position = transform.position;

            do
            {
                // Generate a random angle between 0 and 360 degrees
                var angle = Random.Range(0f, 360f);

                // Convert the angle to radians
                angle *= Mathf.Deg2Rad;
            
                // Calculate the new spawn position
                spawnPosition = new Vector3(
                    position.x + spawnSphereRadius * Mathf.Cos(angle),
                    position.y,
                    position.z + spawnSphereRadius * Mathf.Sin(angle)
                );

            } while (IsPositionInCameraFrustum(playerCamera, spawnPosition));
        

            // Instantiate the enemy at the new spawn position
            var enemyMovement = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyMovement.target = playerTransform.transform;
        }

        public bool IsPositionInCameraFrustum(Camera cam, Vector3 position)
        {
            // Create a negligible size bounds at the position
            Bounds bounds = new Bounds(position, Vector3.zero);

            // Get the camera's frustum planes
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

            // Check if the bounds intersect with the frustum planes
            return GeometryUtility.TestPlanesAABB(planes, bounds);
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
}