using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<GridManager>();
        gameObject.AddComponent<TargetManager>();
        gameObject.AddComponent<AIManager>();
    }
}
