using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Vector2Int Position { get; protected set; }

    public abstract IPathfindingNode GetPathfindingNode();
}
