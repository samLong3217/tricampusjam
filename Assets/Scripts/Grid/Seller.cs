using UnityEngine;

public class Seller : MonoBehaviour
{
    private void Start()
    {
        Vector3 position = transform.position;
        Vector2Int location = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        GridObject obj = GridManager.Get(location);
        if (obj != null)
        {
            obj.Sell();
        }
        
        Destroy(gameObject);
    }
}
