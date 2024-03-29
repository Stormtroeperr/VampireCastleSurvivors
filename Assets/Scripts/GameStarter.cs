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
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || _isGameStarted) return;
        
        var playerInput1 = _playerInputManager.JoinPlayer(0, 0);
        // _playerInputManager.JoinPlayer(1, 1);
        
        var area = Instantiate(areaPrefab, Vector3.zero, Quaternion.identity);
        var enemySpawner = area.GetComponentInChildren<EnemySpawner>();

        enemySpawner.playerCamera = playerInput1.camera;
        enemySpawner.playerTransform = playerInput1.transform;
        
        _isGameStarted = true;
    }
}