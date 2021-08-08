using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingNode
{
    /// <summary>
    /// The grid location of this node
    /// </summary>
    public Vector2Int Location();
    
    /// <summary>
    /// The cost to enter this node
    /// </summary>
    public float Weight();
    
    /// <summary>
    /// The nodes which can be entered from this node
    /// </summary>
    public IEnumerable<IPathfindingNode> Neighbors();
}
