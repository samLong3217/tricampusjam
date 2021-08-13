using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData")]
public class MapData : ScriptableObject
{
    public RectInt PlayRegion;
    public int MaxCrops = 5;
    public int StartMoney = 400;

    public AudioClip PrewaveMusic;
    public AudioClip WaveMusic;
}
