using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private static TargetManager _instance;
    private HashSet<GridObject> _targets;
    
    private void Awake() {
        Destroy(_instance);
        _instance = this;

        _targets = new HashSet<GridObject>();
    }
    
    /// <summary>
    /// Registers an object to the target list.
    /// Automatically registers to the GridManager.
    /// Registration will fail if it fails for the GridManager.
    /// </summary>
    /// <param name="toRegister">The object to register</param>
    /// <returns>Whether the registration was successful</returns>
    public static bool Register(GridObject toRegister)
    {
        if (GridManager.Register(toRegister))
        {
            _instance._targets.Add(toRegister);
            AIManager.ClearPaths();
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Unregisters an object from the target list
    /// Automatically unregisters from the GridManager.
    /// Unregistration will fail if the object is not in the target list or if it fails for the GridManager.
    /// </summary>
    /// <param name="toUnregister">The object to unregister</param>
    /// <returns>Whether the object was successfully unregistered</returns>
    public static bool Unregister(GridObject toUnregister)
    {
        if (!_instance._targets.Remove(toUnregister)) return false;
        bool result = GridManager.Unregister(toUnregister);
        
        if (result)
        {
            AIManager.ClearPaths();
        }
        
        return result;
    }

    /// <summary>
    /// Gets a set of the locations of the targets
    /// </summary>
    public static HashSet<Vector2Int> GetLocations()
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        foreach (GridObject target in _instance._targets)
        {
            result.Add(target.Location);
        }

        return result;
    }
}
