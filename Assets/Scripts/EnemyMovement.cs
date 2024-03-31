using Health;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MovementType
{
    WALKING,
    FLYING,
    DIGGING
}

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;
    
    [SerializeField] private  Vector3 targetPosition;
    [SerializeField] private Vector3 currentRandomPosition;
    [SerializeField] private float mapSize = 10f;
    
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private MovementType movementType;
    
    private Rigidbody _rb;

    public Transform Target
    {
        set => target = value;
    }

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        if (!(bool)target)
        {
            // Failsafe in case the target is not set in the inspector.
            target = FindObjectOfType<PlayerHealth>().gameObject.transform;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        var direction = (targetPosition - _rb.position).normalized;
        var currentXRotation = _rb.rotation.eulerAngles.x;

        var targetRotation = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Lerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

        var rotation = _rb.rotation.eulerAngles;
        rotation.x = currentXRotation;
        _rb.rotation = Quaternion.Euler(rotation);
    }
    
    private void MoveTowardsTarget()
    {
        targetPosition = GetTargetPositionOrRandom();
        
        var direction = targetPosition - _rb.position;
        direction.Normalize();
        
        var speedDeltaTime = speed * Time.fixedDeltaTime;
        var newPosition = _rb.position + direction * speedDeltaTime;
        
        _rb.MovePosition(newPosition);
    }

    private Vector3 GetTargetPositionOrRandom()
    {
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
