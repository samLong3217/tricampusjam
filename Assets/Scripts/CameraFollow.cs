using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    
    public float speed = 2.0f;

    void Start()
    {
        float z = transform.position.z;
        transform.position = objectToFollow.transform.position;  
        Vector3 position = transform.position;
        position.z = z;   
        transform.position = position;
    }
    
    void LateUpdate () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
        
        this.transform.position = position;
    }
}
