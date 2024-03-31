using Player;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    private PlayerJoinHandler _playerJoinHandler;

    [SerializeField] private float offsetLevelsDistance = 1200f;

    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] private PlayerSpawner playerSpawner;
    
    private void Awake()
    {
        _playerJoinHandler = FindObjectOfType<PlayerJoinHandler>();
    }

    private void Start()
    {
        var playerCount = _playerJoinHandler.GetPlayerCount();
        for (var i = 0; i < playerCount; i++)
        {
            var terrainInstance = Instantiate(terrainPrefab, new Vector3(0, 0, i * offsetLevelsDistance), Quaternion.identity);
            var terrainScript = terrainInstance.GetComponent<Terrain>();

            terrainScript.PlayerSpawner.SpawnPlayer();
        }
    }
}
