using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<WaveData> Waves;

    private int _activeWaveIndex;
    private WaveData _activeWave;
    private float[] _timer;
    private int[] _spawnsLeft;

    private void Start()
    {
        SetWave(0);
    }

    private void Update()
    {
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

    private void SetWave(int index)
    {
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

    private void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, this.transform.position, Quaternion.identity);
    }
}
