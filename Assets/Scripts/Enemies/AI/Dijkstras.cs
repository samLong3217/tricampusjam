using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Dijkstras
{
    /// <summary>
    /// Runs Dijkstra's. Returns null if no path is found.
    /// </summary>
    /// <param name="startNode">The start of the path</param>
    /// <param name="endLocs">A collection containing the possible ends of the paths</param>
    /// <returns>An ordered list of the nodes, representing the path. Includes the start and end.</returns>
    public static List<IPathfindingNode> GetPath(IPathfindingNode startNode, HashSet<Vector2Int> endLocs)
    {
        if (endLocs == null || endLocs.Count == 0)
        {
            Debug.Log("Empty Dijkstra");
            return null;
        }
        
        /*
         * This linkedlist setup is used to efficiently store all the paths to search. It does this by storing the
         * current path /backwards/, which is to say each node points to the one /before/ it.
         *               v-- C
         *     start <-- B
         *       ^-- A   ^-- D <-- E <-- end
         *                   ^-- F <-- G
         * (After two iterations, toVisit might look like {A, C, D}.)
         * 
         * Storing in the forward direction would require either a new copy of the path for each candidate, or a data
         * structure with very sophisticated branching.
         * 
         * Once an end is found, it suffices to create a List which is just the linkedlist in reverse order.
         *     head -> end --> E --> D --> B --> start
         *     [start, B, D, E, end]
         */
        SortedSet<LinkedListNode> toVisit = new SortedSet<LinkedListNode>();
        // Prevent loops by preventing nodes from ever being traversed twice
        HashSet<Vector2Int> seen = new HashSet<Vector2Int> {startNode.Location()};

        LinkedListNode nextVisit = new LinkedListNode(startNode, 0, null);
        while (!endLocs.Contains(nextVisit.PathfindingNode.Location()))
        {
            // Add neighbors
            foreach (IPathfindingNode neighbor in nextVisit.PathfindingNode.Neighbors())
            {
                if (seen.Contains(neighbor.Location())) continue;
                
                toVisit.Add(new LinkedListNode(neighbor, nextVisit.PathWeight + neighbor.Weight(), nextVisit));
                seen.Add(neighbor.Location());
            }

            // If there's nothing left to check, return null to indicate no path exists
            if (toVisit.Count == 0) return null;
            
            // Else, continue on as normal
            nextVisit = toVisit.Min;
            toVisit.Remove(nextVisit);
        }

        // Make the List from the linkedlist, reverse order as described above
        List<IPathfindingNode> result = new List<IPathfindingNode>();
        nextVisit.FillList(result);
        return result;
    }
    
    /// <summary>
    /// Runs Dijkstra's. Returns null if no path is found.
    /// </summary>
    /// <param name="start">The start of the path</param>
    /// <param name="end">The end of the path</param>
    /// <returns>An ordered list of the nodes, representing the path. Includes the start and end.</returns>
    public static List<IPathfindingNode> GetPath(IPathfindingNode startNode, Vector2Int endLoc)
    {
        return GetPath(startNode, new HashSet<Vector2Int> {endLoc});
    }

    /*
     * Check the comment in GetPath(IDijkstraNode, HashSet<IDijkstraNode>) to see what this does.
     */
    private class LinkedListNode : IComparable<LinkedListNode>
    {
        public readonly float PathWeight;
        public readonly IPathfindingNode PathfindingNode;
        private readonly LinkedListNode _next;

        public LinkedListNode(IPathfindingNode pathfindingNode, float pathWeight, LinkedListNode next)
        {
            PathfindingNode = pathfindingNode;
            PathWeight = pathWeight;
            _next = next;
        }

        public void FillList(List<IPathfindingNode> list)
        {
            _next?.FillList(list);
            list.Add(PathfindingNode);
        }

        public int CompareTo(LinkedListNode other)
        {
            if (other == null) return 1;
            int comparison = PathWeight.CompareTo(other.PathWeight);
            if (comparison != 0) return comparison;
            comparison = PathfindingNode.Location().x.CompareTo(other.PathfindingNode.Location().x);
            if (comparison != 0) return comparison;
            return PathfindingNode.Location().y.CompareTo(other.PathfindingNode.Location().y);
        }
    }
}
