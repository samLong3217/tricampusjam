using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHover : MonoBehaviour
{
    public float distance = 10.0f;
    public float speed = 3.0f;
    private float origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = origin + Mathf.PingPong(Time.time * speed, distance) - (distance / 2);
        transform.position = pos;
    }
}
