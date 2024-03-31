using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMovement playerMovement;
    
    
        public PlayerMovement PlayerMovement
        {
            get => playerMovement;
            set => playerMovement = value;
        }
    
    
        private InputActionAsset _inputActionAsset;
        private InputActionMap _inputPlayer;
        private InputAction _moveAction;
    
        private void Awake()
        {
            _inputActionAsset = GetComponent<PlayerInput>().actions;
            _inputPlayer = _inputActionAsset.FindActionMap("PlayerMovement");
        }

        private void Update()
        {
            if (!(bool)playerMovement) return;
        
            playerMovement.moveDirection = _moveAction.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            _moveAction = _inputPlayer.FindAction("Move");
            _inputPlayer.Enable();
        }

        private void OnDisable()
        {
            _inputPlayer.Disable();
        }
    }
}
