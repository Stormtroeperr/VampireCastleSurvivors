using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Canvas playerWorldCanvas;
        
        [SerializeField] private GameObject playerPrefab;
    
        private GameObject _spawnedPlayer;
    

    
        public void SpawnPlayer()
        {
            _spawnedPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}