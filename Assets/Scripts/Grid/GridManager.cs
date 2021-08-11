using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    private Dictionary<Vector2Int, GridObject> _objects;
    private RectInt _validRegion;

    /// <summary>
    /// Sets the region in which this GridManager is allowed to handle objects
    /// </summary>
    /// <param name="validRegion"></param>
    public static void SetRect(RectInt validRegion)
    {
        _instance._validRegion = new RectInt(validRegion.position, validRegion.size + Vector2Int.one);
    }

    private void Awake()
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
        if (!_instance._validRegion.Contains(toRegister.Location)) return false;
        
        if (_instance._objects.TryGetValue(toRegister.Location, out GridObject existing) && existing != null)
        {
            return false;
        }
        
        _instance._objects.Add(toRegister.Location, toRegister);
        AIManager.ClearPaths();
        return true;
    }
    
    /// <summary>
    /// Unregisters an object to the grid.
    /// You must use this method to ensure the object stops interacting with the game.
    /// </summary>
    /// <param name="toUnregister">The object to unregister</param>
    /// <returns>Whether the object was successfully unregistered</returns>
    public static bool Unregister(GridObject toUnregister)
    {
        if (!_instance._objects.TryGetValue(toUnregister.Location, out GridObject current) ||
            current != toUnregister) return false;
        bool result = _instance._objects.Remove(toUnregister.Location);
        if (result) {
            AIManager.ClearPaths();
        }
        return result;
    }

    /// <summary>
    /// Gets the object at a given location.
    /// If there is no object at that location, returns null.
    /// </summary>
    /// <param name="location">The location to check in</param>
    /// <returns>The object or null</returns>
    public static GridObject Get(Vector2Int location)
    {
        if (!_instance._validRegion.Contains(location)) return null;
        return _instance._objects.TryGetValue(location, out GridObject existing) ? existing : null;
    }
    
    /// <summary>
    /// Determines if the given location is inside the play area
    /// </summary>
    public static bool IsValidLocation(Vector2Int location)
    {
        return _instance._validRegion.Contains(location);
    }

    private void OnDestroy()
    {
        foreach (GridObject obj in _instance._objects.Values)
        {
            if (obj != null) Destroy(obj.gameObject);
        }
    }
}
