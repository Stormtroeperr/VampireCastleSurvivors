using Player;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    
    public PlayerSpawner PlayerSpawner => playerSpawner;
}