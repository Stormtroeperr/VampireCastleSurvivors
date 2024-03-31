using UnityEngine;

public enum MovementType
{
    WALKING,
    FLYING,
    DIGGING
}

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    
    
    [SerializeField] private  Vector3 _targetPosition;
    
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float mapSize = 10f;
    
    [SerializeField] private Vector3 currentRandomPosition;
    
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private MovementType movementType;
    
    private Rigidbody _rb;
    
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        MoveTowardsTarget();
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        var direction = (_targetPosition - _rb.position).normalized;
        var currentXRotation = _rb.rotation.eulerAngles.x; // Store the current X rotation

        var targetRotation = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Lerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

        var rotation = _rb.rotation.eulerAngles;
        rotation.x = currentXRotation; // Set the X rotation back to its original value
        _rb.rotation = Quaternion.Euler(rotation);
    }
    
    private void MoveTowardsTarget()
    {
        _targetPosition = GetTargetPositionOrRandom();
        
        var direction = _targetPosition - _rb.position;
        direction.Normalize();
        
        var speedDeltaTime = speed * Time.fixedDeltaTime;
        var newPosition = _rb.position + direction * speedDeltaTime;
        
        _rb.MovePosition(newPosition);
    }

    private Vector3 GetTargetPositionOrRandom()
    {
        // Check if the target exists and if the target is active
        if ((bool)target && target.gameObject.activeSelf)
        {
            return target.position;
        }

        if (!(Vector3.Distance(_rb.position, currentRandomPosition) < 1f)) return currentRandomPosition;
        
        currentRandomPosition = GenerateRandomPosition();
        return currentRandomPosition;
    }
    
    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-mapSize / 2, mapSize / 2), 1, Random.Range(-mapSize / 2, mapSize / 2));
    }
}
