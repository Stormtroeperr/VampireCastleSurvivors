using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class PlayerMovement : Movement
{
    
    private Movement _movement;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float detectionRadius = 10f;
    
    private InputActionAsset _inputActionAsset;
    private InputActionMap _inputPlayer;

    [SerializeField] private Transform modelTransform;

    public Vector2 moveDirection;

    private CharacterController _cc;
    
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _movement = GetComponent<Movement>();
        
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _inputPlayer = _inputActionAsset.FindActionMap("PlayerMovement");
    }

    private void Update()
    {
        if(!_cc.enabled) return;
        
        _movement.SetVelocity(new Vector3(moveDirection.x, 0, moveDirection.y) * speed);
        LookAtClosestEnemy();
    }
    
    private void OnEnable()
    {
        _inputPlayer.Enable();
    }

    private void OnDisable()
    {
        _inputPlayer.Disable();
    }

    
    public void Move(InputAction.CallbackContext ctx)
    {
        moveDirection = ctx.ReadValue<Vector2>();
    }
    
    private void LookAtClosestEnemy()
    {
        //TODO: Change this later since it's not efficient to find all enemies every frame.
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestEnemy = null;
        var closestDistance = detectionRadius;

        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(modelTransform.position, enemy.transform.position);
            if (!(distance < closestDistance)) continue;
            closestDistance = distance;
            closestEnemy = enemy;
        }

        if ((bool)closestEnemy && closestDistance <= detectionRadius)
        {
            var directionToEnemy = (closestEnemy.transform.position - modelTransform.position).normalized;
            var lookAtEnemyRotation = Quaternion.LookRotation(directionToEnemy);

            var rotation = modelTransform.rotation;
            lookAtEnemyRotation.x = rotation.x;
            lookAtEnemyRotation.z = rotation.z;

            rotation = Quaternion.Lerp(rotation, lookAtEnemyRotation, Time.deltaTime * rotationSpeed);
            modelTransform.rotation = rotation;
            return;
        }

        var flatMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

        // Only change the rotation if the player is actually moving
        if (!(flatMoveDirection.sqrMagnitude > 0.01f)) return;

        var lookAtWalkingRotation = Quaternion.LookRotation(flatMoveDirection);
        modelTransform.rotation =
            Quaternion.Lerp(modelTransform.rotation, lookAtWalkingRotation, Time.deltaTime * rotationSpeed);
    }
}