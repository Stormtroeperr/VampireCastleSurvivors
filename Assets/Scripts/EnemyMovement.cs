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
    
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float mapSize = 10f;
    [SerializeField] private Vector3 currentRandomPosition;
    [SerializeField] private MovementType movementType;
    
    private Rigidbody _rb;
    
    // TODO: Get assigned a target from the GameManager
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void FixedUpdate()
    { 
        MoveTowardsTarget();
    }
    
    private void MoveTowardsTarget()
    {
        var direction = GetTargetPositionOrRandom() - _rb.position;
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
        
        if (Vector3.Distance(_rb.position, currentRandomPosition) < 1f)
        {
            currentRandomPosition = GenerateRandomPosition();
        }

        return currentRandomPosition;
    }
    
    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-mapSize / 2, mapSize / 2), 0, Random.Range(-mapSize / 2, mapSize / 2));
    }
}
