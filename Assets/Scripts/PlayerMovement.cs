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
    
    [SerializeField]
    private Transform modelTransform;
    
    private void Awake()
    {
        _movement = GetComponent<Movement>();
        
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _inputPlayer = _inputActionAsset.FindActionMap("PlayerMovement"); 
        _moveAction = _inputPlayer.FindAction("Move");
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
    }

    private void FixedUpdate()
    {
        var yVel = _movement.GetVelocity().y;
        _movement.SetVelocity(new Vector3(_moveDirection.x, yVel, _moveDirection.y) * speed);
    }

    private void Update()
    {
        LookAtClosestEnemy();
    }

    private void LookAtClosestEnemy()
    {
        const float detectionRadius = 100f;
        var hitColliders = new Collider[10];

        var numColliders = Physics.OverlapSphereNonAlloc(modelTransform.position, detectionRadius, hitColliders);

        var closestDistance = detectionRadius;

        Transform closestEnemy = null;

        for (var i = 0; i < numColliders; i++)
        {
            if (!hitColliders[i].gameObject.CompareTag("Enemy")) continue;

            var distance = Vector3.Distance(modelTransform.position, hitColliders[i].transform.position);

            if (!(distance < closestDistance)) continue;

            closestDistance = distance;
            closestEnemy = hitColliders[i].transform;
        }

        if (!(bool)closestEnemy) return;
        

        var directionToEnemy = (closestEnemy.position - modelTransform.position).normalized;
        var lookRotation = Quaternion.LookRotation(directionToEnemy);
        
        var rotation = modelTransform.rotation;
        
        lookRotation.x = rotation.x;
        lookRotation.z = rotation.z;
        
        rotation = lookRotation;
        modelTransform.rotation = rotation;
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
