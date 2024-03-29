using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer<Collider>
{
    private readonly Vector3 _position;

    public DistanceComparer(Vector3 position)
    {
        _position = position;
    }

    public int Compare(Collider x, Collider y)
    {
        if (x == null || y == null)
        {
            return 0;
        }

        var distanceToX = Vector3.Distance(_position, x.transform.position);
        var distanceToY = Vector3.Distance(_position, y.transform.position);

        return distanceToX.CompareTo(distanceToY);
    }
}