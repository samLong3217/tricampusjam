public interface IDijkstraNode : System.IEquatable<IDijkstraNode>
{
    /// <summary>
    /// The cost to enter this node
    /// </summary>
    public float Weight();
    
    /// <summary>
    /// The nodes which can be entered from this node
    /// </summary>
    public System.Collections.Generic.List<IDijkstraNode> Neighbors();
}
