using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class GameStarter : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private bool _isGameStarted;

    [SerializeField] private GameObject areaPrefab;
    
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
        
        _isGameStarted = true;
    }
}