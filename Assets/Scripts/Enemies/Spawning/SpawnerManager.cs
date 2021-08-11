using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private static SpawnerManager _instance;

    private HashSet<EnemySpawner> _spawners;
    
    private void Awake()
    {
        Destroy(_instance);
        _instance = this;

        _spawners = new HashSet<EnemySpawner>();
    }

    /// <summary>
    /// Registers a spawner.
    /// Registration will fail if an object already exists at the desired location.
    /// </summary>
    /// <param name="toRegister">The spawner to register</param>
    /// <returns>Whether the registration was successful</returns>
    public static bool Register(EnemySpawner toRegister)
    {
        return _instance._spawners.Add(toRegister);
    }

    /// <summary>
    /// Unregisters a spawner.
    /// </summary>
    /// <param name="toUnregister">The object to unregister</param>
    /// <returns>Whether the object was successfully unregistered</returns>
    public static bool Unregister(EnemySpawner toUnregister)
    {
        return _instance._spawners.Remove(toUnregister);
    }

    /// <summary>
    /// Sets all spawners' waves and wait times.
    /// </summary>
    /// <param name="index">The wave to set all spawners on</param>
    /// <param name="waitTime">The amount of wait time for all spawners</param>
    public static void SetAllWaves(int index, float waitTime = 0)
    {
        foreach (EnemySpawner spawner in _instance._spawners)
        {
            if (spawner == null) continue;
            Debug.Log("Setting wave for spawner...");
            spawner.SetWave(index, waitTime);
        }
    }

    /// <summary>
    /// Increments all spawners' waves.
    /// </summary>
    /// <param name="waitTime">The amount of time to wait for all spawners</param>
    public static void IncrementAllWaves(float waitTime = 0)
    {
        foreach (EnemySpawner spawner in _instance._spawners)
        {
            if (spawner == null) continue;
            spawner.IncrementWave(waitTime);
        }
    }

    /// <summary>
    /// Checks if all spawners have completed spawning for the current wave.
    /// </summary>
    public static bool AllSpawnersDoneCurrent()
    {
        return _instance._spawners.All(s => s == null || s.DoneSpawningCurrent());
    }

    /// <summary>
    /// Checks if all spawners have another wave to spawn.
    /// </summary>
    public static bool AllSpawnersHaveAnother()
    {
        return _instance._spawners.All(s => s == null || s.HasAnotherWave());
    }

    /// <summary>
    /// A float from 0 to 1 representing how much of this wave has been completed
    /// </summary>
    public static float Completion()
    {
        HashSet<EnemySpawner> spawners = _instance._spawners;
        float total = spawners.Sum(x => x.WaveSpawnCount());
        if (total == 0) return 0;
        float remaining = spawners.Sum(x => x.RemainingSpawnCount());
        return 1 - remaining / total;
    }
}
