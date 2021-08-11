using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance;

    public MapData MapData;
    
    private void Awake()
    {
        Instance = this;
        
        gameObject.AddComponent<GridManager>();
        gameObject.AddComponent<TargetManager>();
        gameObject.AddComponent<AIManager>();
        gameObject.AddComponent<SpawnerManager>();
        gameObject.AddComponent<RoundManager>();
        gameObject.AddComponent<MoneyManager>();
    }
}
