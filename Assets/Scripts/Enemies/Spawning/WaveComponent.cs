using UnityEngine;

[System.Serializable]
public struct WaveComponent
{
    /// <summary>
    /// The spawn rate, in enemies per second
    /// </summary>
    public float Rate;

    /// <summary>
    /// The number of enemies to spawn in this wave
    /// </summary>
    public int Count;

    /// <summary>
    /// The enemy to spawn in this wave
    /// </summary>
    public GameObject Prefab;
}
