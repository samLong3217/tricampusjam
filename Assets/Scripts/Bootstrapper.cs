using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<GridManager>();
        gameObject.AddComponent<TargetManager>();
        gameObject.AddComponent<AIManager>();
    }
}
