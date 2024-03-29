using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    private MeshCollider _swordCollider;
    [SerializeField] private Mesh swordMesh;
    
    protected void Start()
    {
        _swordCollider = gameObject.AddComponent<MeshCollider>();
        _swordCollider.sharedMesh = swordMesh;
        _swordCollider.convex = true;
    }

    private Mesh CreateCuboidMesh(float length, float width, float height)
{
    var mesh = new Mesh();

    var vertices = new Vector3[24]
    {
        new(-length * .5f, -width * .5f, height * .5f),
        new(length * .5f, -width * .5f, height * .5f),
        new(length * .5f, -width * .5f, -height * .5f),
        new(-length * .5f, -width * .5f, -height * .5f),
        new(-length * .5f, width * .5f, height * .5f),
        new(length * .5f, width * .5f, height * .5f),
        new(length * .5f, width * .5f, -height * .5f),
        new(-length * .5f, width * .5f, -height * .5f),
        new(-length * .5f, -width * .5f, height * .5f),
        new(length * .5f, -width * .5f, height * .5f),
        new(length * .5f, width * .5f, height * .5f),
        new(-length * .5f, width * .5f, height * .5f),
        new(-length * .5f, -width * .5f, -height * .5f),
        new(length * .5f, -width * .5f, -height * .5f),
        new(length * .5f, width * .5f, -height * .5f),
        new(-length * .5f, width * .5f, -height * .5f),
        new(-length * .5f, width * .5f, -height * .5f),
        new(-length * .5f, width * .5f, height * .5f),
        new(-length * .5f, -width * .5f, height * .5f),
        new(-length * .5f, -width * .5f, -height * .5f),
        new(length * .5f, width * .5f, -height * .5f),
        new(length * .5f, width * .5f, height * .5f),
        new(length * .5f, -width * .5f, height * .5f),
        new(length * .5f, -width * .5f, -height * .5f)
    };

    int[] triangles = {
        0, 2, 1, //face front
        0, 3, 2,
        2, 3, 4, //face top
        2, 4, 5,
        1, 2, 5, //face right
        1, 5, 6,
        0, 7, 4, //face left
        0, 4, 1,
        5, 4, 7, //face back
        5, 7, 6,
        0, 6, 7, //face bottom
        0, 1, 6
    };

    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();

    return mesh;
}
    
    public void Attack()
    {
        EnableCollider();

        DisableCollider();
    }
}
