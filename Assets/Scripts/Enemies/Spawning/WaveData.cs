using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]
public class WaveData : ScriptableObject
{
    public List<WaveComponent> Components;

    public WaveComponent this[int i] => Components[i];

    public int Count => Components.Count;
}
