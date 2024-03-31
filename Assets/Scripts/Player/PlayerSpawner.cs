using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
            LinkHealthBarWithPlayer(_spawnedPlayer.GetComponent<PlayerInput>(), _spawnedPlayer);
        }
    
    
        private void LinkHealthBarWithPlayer(PlayerInput playerInput, GameObject player)
        {
            var canvasInstance = Instantiate(playerWorldCanvas, Vector3.zero, Quaternion.identity);
        
            // Assign the player's camera to the player's canvas so we can see the canvas
            var parentHealthBar = canvasInstance.GetComponentInChildren<Image>();

            //Give the player a reference to the image
            var playerHealth = player.GetComponentInChildren<PlayerHealth>();
            playerHealth.SetHealthBar(parentHealthBar.gameObject);
        
            //Set the camera to the right canvas
            canvasInstance.GetComponent<Canvas>().worldCamera = playerInput.camera;
        }
    }
}