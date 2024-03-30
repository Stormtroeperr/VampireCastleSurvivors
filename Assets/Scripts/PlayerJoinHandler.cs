using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerJoinHandler : MonoBehaviour, IPlayerJoinListener
{
    private PlayerInputManager _playerInputManager;
    private bool _isGameStarted;
    
    private int _spawnedPlayers;
    
    // List of IPlayerJoinListener instances
    private List<IPlayerJoinListener> _playerJoinListeners = new();
    
    private void Awake  ()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }
    
    public void OnPlayerJoined(PlayerInput player)
    {
        foreach (var listener in _playerJoinListeners)
        {
            listener.OnPlayerJoined(player);
        }
        
        _spawnedPlayers++;
        
        if (_spawnedPlayers == 2)
        {
            _playerInputManager.DisableJoining();
        }
    }
    
    // Method to add a listener to the list
    public void AddPlayerJoinListener(IPlayerJoinListener listener)
    {
        _playerJoinListeners.Add(listener);
    }

    // Method to remove a listener from the list
    public void RemovePlayerJoinListener(IPlayerJoinListener listener)
    {
        _playerJoinListeners.Remove(listener);
    }
}
