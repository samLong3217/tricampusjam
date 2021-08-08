using System;
using System.Collections.Generic;

public interface IPathfindingNode : IEquatable<IPathfindingNode>
{
    /// <summary>
    /// The cost to enter this node
    /// </summary>
    public float Weight();
    
    /// <summary>
    /// The nodes which can be entered from this node
    /// </summary>
    public IEnumerable<IPathfindingNode> Neighbors();
}
