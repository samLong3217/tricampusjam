using UnityEngine;

public class PlayerTarget : GridObject
{
    protected new void Start()
    {
        TargetManager.Register(this);
        GridManager.Unregister(this);
        DebugMode.Enabled = true;
    }
    
    public override IPathfindingNode GetPathfindingNode()
    {
        return new BasicPathfindingNode(Location);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector2Int newLocation = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        if (newLocation != Location) AIManager.ClearPaths();
        Location = newLocation;
    }
}
