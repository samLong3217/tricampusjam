using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    private bool _registered = false;
    
    protected virtual void Start()
    {
        Vector3 position = transform.position;
        Location = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        if (!GridManager.Register(this))
        {
            OnRegister(false);
            Destroy(gameObject);
        }
        else
        {
            _registered = true;
            OnRegister(true);
        }
    }
    
    public Vector2Int Location { get; protected set; }

    public abstract IPathfindingNode GetPathfindingNode();

    protected virtual void OnRegister(bool success)
    {
        
    }

    protected virtual void OnDestroy()
    {
        if (_registered) GridManager.Unregister(this);
    }
}
