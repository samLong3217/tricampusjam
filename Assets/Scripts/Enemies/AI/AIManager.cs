using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static bool DebugMode = false;
    
    private static AIManager _instance;
    private Dictionary<Vector2Int, AINode> _pathNodes;
    
    private void Awake() {
        Destroy(_instance);
        _instance = this;

        _pathNodes = new Dictionary<Vector2Int, AINode>();
    }

    /// <summary>
    /// Runs pathfinding for the given enemy
    /// </summary>
    /// <param name="enemy">The enemy that wants to move</param>
    /// <returns>The location of the target</returns>
    public static Vector2Int GetTarget(Enemy enemy)
    {
        Vector2 enemyPos = (Vector2) enemy.transform.position + enemy.Rigidbody2d.velocity * Time.deltaTime;
        Vector2Int enemyLoc = new Vector2Int(Mathf.RoundToInt(enemyPos.x), Mathf.RoundToInt(enemyPos.y));

        if (!_instance._pathNodes.ContainsKey(enemyLoc))
        {
            if (!MakePath(enemyLoc, TargetManager.GetLocations()))  // If pathmaking failed
            {
                return enemyLoc;
            }
        }

        AINode node0 = _instance._pathNodes[enemyLoc];
        // We're getting the location even though it should be the same as enemyLoc, for consistency.
        Vector2Int loc0 = node0.PathfindingNode.Location();
        if (node0.Next == null) return loc0;
        Vector2Int loc1 = node0.Next.PathfindingNode.Location();
        
        // Move to node0 if node1 is far away. Else move to node1.
        float distance0To1 = Vector2Int.Distance(loc0 ,loc1);
        float distanceEnemyTo1 = Vector2.Distance(enemyPos, loc1);
        return distance0To1 + 0.1f < distanceEnemyTo1 ? loc0 : loc1;
    }

    /// <summary>
    /// Clears all paths
    /// </summary>
    public static void ClearPaths()
    {
        _instance._pathNodes.Clear();
    }

    private static bool MakePath(Vector2Int startLoc, HashSet<Vector2Int> endLocs)
    {
        GridObject startObject = GridManager.Get(startLoc);
        IPathfindingNode startNode = startObject != null ?
            startObject.GetPathfindingNode() : new BasicPathfindingNode(startLoc);
        
        List<IPathfindingNode> path = Dijkstras.GetPath(startNode, endLocs);
        if (path == null) return false;
        
        // Add the end
        IPathfindingNode end = path[path.Count - 1];
        // The node at the end of the processed path
        AINode lastNode;
        // Try to set lastNode to the node at the end loc. If that doesn't exist, make it.
        if (!_instance._pathNodes.TryGetValue(end.Location(), out lastNode))
        {
            lastNode = new AINode(end, null);
            _instance._pathNodes[end.Location()] = lastNode;
        }
        
        // Add nodes starting from the second-to-last and working backwards
        for (int i = path.Count - 2; i >= 0; i--)
        {
            if (_instance._pathNodes.ContainsKey(path[i].Location()))
            {
                lastNode = _instance._pathNodes[path[i].Location()];
            }
            else
            {
                lastNode = new AINode(path[i], lastNode);
                _instance._pathNodes[path[i].Location()] = lastNode;
            }

            if (DebugMode)
            {
                Vector2 loc0 = path[i].Location();
                Vector2 loc1 = path[i + 1].Location();
                Debug.DrawLine(loc0,loc1, Color.red, 5, false);
            }
        }

        return true;
    }
    
    // Standard linkedlist node
    private class AINode
    {
        public readonly IPathfindingNode PathfindingNode;
        public readonly AINode Next;
        
        public AINode(IPathfindingNode pathfindingNode, AINode next)
        {
            PathfindingNode = pathfindingNode;
            Next = next;
        }
    }
}
