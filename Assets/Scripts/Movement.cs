using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;

    public void SetVelocity(Vector3 vel)
    {
        _cc.Move(vel);
    }
    
    public Vector3 GetVelocity()
    {
        return _cc.velocity;
    }
}
