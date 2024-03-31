using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerJoinHandler : MonoBehaviour, IPlayerJoinListener
    {
        private PlayerInputManager _playerInputManager;
        private bool _isGameStarted;
    
        public List<PlayerInput> _playerInputHandler = new();
    
        // List of IPlayerJoinListener instances
        private readonly List<IPlayerJoinListener> _playerJoinListeners = new();
    
        private void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
        }
    
        // OnPlayerJoined method receives a PlayerInput instance and adds it to the list.
        // this instance is a PlayerInputHandler but not the player prefab itself.
        public void OnPlayerJoined(PlayerInput player)
        {
            _playerInputHandler.Add(player);

            // Notify all listeners that a player has joined
            foreach (var listener in _playerJoinListeners)
            {
                listener.OnPlayerJoined(player);
            }
        
            if (_playerInputHandler.Count == 2)
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
    
        public int GetPlayerCount()
        {
            return _playerInputHandler.Count;
        }
        
        public PlayerInput[] GetPlayerInputHandlers()
        {
            return _playerInputHandler.ToArray();
        }
    }
}
