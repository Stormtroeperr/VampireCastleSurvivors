using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInputManager))]
public class GameStarter : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private bool _isGameStarted;

    [SerializeField] private GameObject areaPrefab;
    [SerializeField] private GameObject playerWorldCanvas;
    
    private void Awake  ()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }
    public void OnPlayerJoined(PlayerInput player)
    {
        if (_isGameStarted) return;
        
        var area = Instantiate(areaPrefab, Vector3.zero, Quaternion.identity);
        var enemySpawner = area.GetComponentInChildren<EnemySpawner.EnemySpawner>();
        
        enemySpawner.playerCamera = player.camera;
        enemySpawner.playerTransform = player.transform;
        
        LinkHealthBarWithPlayer(player, area);
        
        _isGameStarted = true;
    }

    private void LinkHealthBarWithPlayer(PlayerInput player, GameObject area)
    {
        var canvasInstance = Instantiate(playerWorldCanvas, Vector3.zero, Quaternion.identity);
        
        // Assign the player's camera to the player's canvas so we can see the canvas
        var parentHealthBar = canvasInstance.GetComponentInChildren<Image>();

        //Give the player a reference to the image
        var playerHealth = player.GetComponentInChildren<PlayerHealth>();
        playerHealth.SetHealthBar(parentHealthBar.gameObject);
        
        //Set the camera to the right canvas
        canvasInstance.GetComponent<Canvas>().worldCamera = player.camera;
    }
}