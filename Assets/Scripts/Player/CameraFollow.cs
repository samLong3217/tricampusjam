using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public static Camera MainCamera;
    
    public GameObject objectToFollow;
    
    public float speed = 2.0f;

    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
    }
    
    void Update () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
        
        this.transform.position = position;
    }
}