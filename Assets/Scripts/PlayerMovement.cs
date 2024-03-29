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

        // Sort the colliders based on their distance to the player
        Array.Sort(hitColliders, 0, numColliders, new DistanceComparer(modelTransform.position));

        // Find the first collider that is tagged as "Enemy"
        foreach (var hitCollider in hitColliders)
        {
            if (!(bool)hitCollider || !hitCollider.CompareTag("Enemy")) continue;
            
            
            var directionToEnemy = (hitCollider.transform.position - modelTransform.position).normalized;
            var lookRotation = Quaternion.LookRotation(directionToEnemy);

            // Keep the player's current x and z rotation and only change the y rotation to look at the enemy
            lookRotation.x = modelTransform.rotation.x;
            lookRotation.z = modelTransform.rotation.z;

            // Interpolate between the player's current rotation and the desired rotation over time
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, lookRotation, Time.deltaTime * speed);

            // Stop looking for enemies as soon as the closest one is found
            break;
        }
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
