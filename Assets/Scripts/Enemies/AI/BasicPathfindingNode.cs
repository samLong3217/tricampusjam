
using System.Collections.Generic;
using UnityEngine;

public class BasicPathfindingNode : IPathfindingNode
{
    private readonly Vector2Int _location;
    private readonly float _weight;
    
    public BasicPathfindingNode(Vector2Int location, float weight = 1)
    {
        _location = location;
        _weight = weight;
    }

    public Vector2Int Location()
    {
        return _location;
    }

    public float Weight()
    {
        return _weight;
    }

    private readonly Vector2Int[] _neighborOffsets =
    {
        new Vector2Int(1,0),
        new Vector2Int(0,1),
        new Vector2Int(-1,0),
        new Vector2Int(0,-1),
    };

    public IEnumerable<IPathfindingNode> Neighbors()
    {
        List<IPathfindingNode> result = new List<IPathfindingNode>();
        foreach (Vector2Int offset in _neighborOffsets)
        {
            if (!GridManager.IsValidLocation(_location + offset)) continue;
            
            GridObject gridObject = GridManager.Get(_location + offset);
            result.Add(gridObject == null
                ? new BasicPathfindingNode(_location + offset)
                : gridObject.GetPathfindingNode());
        }

        return result;
    }
}
