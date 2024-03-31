using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Enemy spawning settings")]
        // We need this here for now so we can easily get the enemies and subscribe to their death events
        [SerializeField] private GameObject areaControllerPrefab;
        
        public void SpawnPlayer(PlayerInput playerInput)
        {
            var areaController = Instantiate(areaControllerPrefab, transform.position, Quaternion.identity);

            var inventory = playerInput.GetComponentInChildren<Inventory>();
            inventory.SetEnemySpawner(areaController);
            
            playerInput.camera.enabled = true;
            playerInput.transform.position = transform.position;
            playerInput.transform.rotation = Quaternion.identity;
            
            
            playerInput.GetComponent<CharacterController>().enabled = true;
        }
    }
}