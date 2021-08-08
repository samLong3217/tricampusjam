public class Wall : GridObject
{
    public override IPathfindingNode GetPathfindingNode()
    {
        return new BasicPathfindingNode(Location, 500);
    }
}
