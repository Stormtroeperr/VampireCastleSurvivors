using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Movement))]
public class PlayerMovement : MonoBehaviour
{
    private Movement _movement;

    [SerializeField]
    private float speed = 5f;

    private Vector2 _moveDirection;
    
    private InputActionAsset _inputActionAsset;
    private InputActionMap _inputPlayer;
    private InputAction _moveAction;
    
    private void Awake()
    {
        _movement = GetComponent<Movement>();
        
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _inputPlayer = _inputActionAsset.FindActionMap("PlayerMovement"); 
        _moveAction = _inputPlayer.FindAction("Move");
        _moveAction.performed += this.Move;
        _moveAction.canceled += this.Move;
    }

    private void FixedUpdate()
    {
        float yVel = _movement.GetVelocity().y;
        _movement.SetVelocity(new Vector3(_moveDirection.x, yVel, _moveDirection.y) * speed);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.ReadValue<Vector2>();      
    }
    
    private void OnEnable()
    {
        _inputPlayer.Enable();
    }
    
    private void OnDisable()
    {
        _inputPlayer.Disable();
    }
}
