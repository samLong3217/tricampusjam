using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<WaveData> Waves;

    private float _waitTime;
    
    private int _activeWaveIndex;
    private WaveData _activeWave;
    private float[] _timer;
    private int[] _spawnsLeft;

    private void Start()
    {
        SpawnerManager.Register(this);
        //SetWave(0);
    }

    private void Update()
    {
        if (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0) SetWave(_activeWaveIndex, 0);
            else return;
        }
        
        if (_activeWave == null) return;
        
        bool hasSpawnsLeft = false;
        
        for (int i = 0; i < _timer.Length; i++)
        {
            if (_spawnsLeft[i] <= 0) continue;
            else hasSpawnsLeft = true;
            
            _timer[i] += Time.deltaTime;
            while (_timer[i] > 1 / _activeWave[i].Rate)
            {
                _timer[i] -= 1 / _activeWave[i].Rate;
                _spawnsLeft[i]--;
                SpawnEnemy(_activeWave[i].Prefab);
            }
        }

        if (!hasSpawnsLeft)
        {
            //SetWave(_activeWaveIndex + 1);
        }
    }

    private void OnDestroy()
    {
        SpawnerManager.Unregister(this);
    }

    /// <summary>
    /// Sets the spawner to the designated wave and starts spawning.
    /// </summary>
    /// <param name="index">The wave to move to</param>
    /// <param name="waitTime">An amount of time to wait before starting to spawn</param>
    public void SetWave(int index, float waitTime = 0)
    {
        _spawnsLeft = new [] {1};
        if (waitTime != 0)
        {
            Debug.Log("Setting wave");
            _activeWaveIndex = index;
            _waitTime = waitTime;
            return;
        }
        
        if (index >= Waves.Count)
        {
            //Destroy(gameObject);
            return;
        }

        _activeWaveIndex = index;
        _activeWave = Waves[index];
        _timer = new float[_activeWave.Count];
        _spawnsLeft = new int[_activeWave.Count];
        for (int i = 0; i < _activeWave.Count; i++)
        {
            _spawnsLeft[i] = _activeWave[i].Count;
        }
    }

    /// <summary>
    /// Sets the spawner to the next wave and starts spawning.
    /// </summary>
    /// <param name="waitTime">An amount of time to wait before starting to spawn</param>
    public void IncrementWave(float waitTime = 0)
    {
        SetWave(_activeWaveIndex + 1, waitTime);
        Debug.Log("Active wave is now " + _activeWaveIndex);
    }

    /// <summary>
    /// Checks if the current wave has completed spawning.
    /// </summary>
    public bool DoneSpawningCurrent()
    {
        return !_spawnsLeft.Any(n => n > 0);
    }

    /// <summary>
    /// Checks if there is a wave after this one.
    /// </summary>
    public bool HasAnotherWave()
    {
        return _activeWaveIndex < Waves.Count - 1;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, this.transform.position, Quaternion.identity);
    }
}
