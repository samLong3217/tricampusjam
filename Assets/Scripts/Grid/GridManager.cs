using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    private Dictionary<Vector2Int, GridObject> _objects;

    private void Start()
    {
        Destroy(_instance);
        _instance = this;

        _objects = new Dictionary<Vector2Int, GridObject>();
    }

    /// <summary>
    /// Registers an object to the grid.
    /// You must use this method to ensure the object can interact with the game.
    /// Registration will fail if an object already exists at the desired location.
    /// </summary>
    /// <param name="toRegister">The object to register</param>
    /// <returns>Whether the registration was successful</returns>
    public static bool Register(GridObject toRegister)
    {
        if (_instance._objects.TryGetValue(toRegister.Position, out GridObject existing) && existing != null)
        {
            return false;
        }
        
        _instance._objects[toRegister.Position] = toRegister;
        return true;
    }

    /// <summary>
    /// Gets the object at a given position.
    /// If there is no object at that position, returns null.
    /// </summary>
    /// <param name="position">The position to check in</param>
    /// <returns>The object or null</returns>
    public static GridObject Get(Vector2Int position)
    {
        return _instance._objects.TryGetValue(position, out GridObject existing) ? existing : null;
    }

    private void OnDestroy()
    {
        foreach (GridObject obj in _instance._objects.Values)
        {
            if (obj != null) Destroy(obj.gameObject);
        }
    }
}
