using UnityEngine;

public class FloatObject : MonoBehaviour, IPickupable
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float rotationSpeed = 50f;
    public Transform playerTransform; // Reference to the player's transform
    public float pickupDistance = 10f; // Distance at which the object starts moving towards the player
    public float moveSpeed = 5f; // Speed at which the object moves towards the player

    public int xpValue = 10;
    
    private Vector3 _posOffset;
    private Vector3 _tempPos;

    void Start()
    {
        _posOffset = transform.position;

        // Find the player object in the scene and assign it as the target
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check the distance to the player
        var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= pickupDistance)
        {
            MoveToTarget(playerTransform);
        }
        else
        {
            _tempPos = _posOffset;
            _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = _tempPos;
        }
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void MoveToTarget(Transform target)
    {
        // Calculate the up and down motion
        _tempPos = _posOffset;
        _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        // Create a new target position that includes the up and down motion
        Vector3 targetPosWithMotion = new Vector3(target.position.x, _tempPos.y, target.position.z);

        // Move towards the new target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosWithMotion, moveSpeed * Time.deltaTime);
    }
}