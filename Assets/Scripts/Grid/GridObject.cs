using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    protected void Start()
    {
        Vector3 position = transform.position;
        Location = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        if (!GridManager.Register(this)) Destroy(gameObject);
    }
    
    public Vector2Int Location { get; protected set; }

    public abstract IPathfindingNode GetPathfindingNode();
}
