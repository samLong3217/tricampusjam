using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    Rigidbody2D body;
    float horizontal;
    float vertical;
    int dir; // cardinal direciton player is facing for tower placement. 0 = E, 1 = N, 2 = W, 3 = S


    public float runSpeed = 20.0f;

    void Start () {
        body = GetComponent<Rigidbody2D>(); 
    }

    void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        // update direciton facing
        if (horizontal != 0) {
            dir = (int) Mathf.Floor(horizontal + 1);
        } else if (vertical != 0) { 
            dir = (int) Mathf.Floor(vertical + 2);
        }

        // update the target reticle position based on direction

    }

    private void FixedUpdate() {  
        body.velocity = new Vector2(horizontal, vertical);
        body.velocity = body.velocity.normalized * runSpeed * Time.deltaTime;
    }

    public int Dir() {
        return dir;
    }
}
