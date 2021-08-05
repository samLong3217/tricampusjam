using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    Rigidbody2D body;
    float horizontal;
    float vertical;
    int dir; // cardinal direciton player is facing for tower placement. 0 = E, 1 = N, 2 = W, 3 = S

    GameObject selector; // Child selector


    public float runSpeed = 20.0f;

    public GameObject towerPrefab;

    void Start () {
        body = GetComponent<Rigidbody2D>(); 
        
        Transform[] transforms = this.GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms) {
            if (t.gameObject.name == "Selector") {
                selector  = t.gameObject;
            }
        }
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

        // Place a tower when the button is pressed
        if (Input.GetButtonDown("Jump")) {
            PlaceTower();
        }

    }

    private void FixedUpdate() {  
        body.velocity = new Vector2(horizontal, vertical);
        body.velocity = body.velocity.normalized * runSpeed * Time.deltaTime;
    }

    public int Dir() {
        return dir;
    }

    // Places a tower at the current selector position
    public void PlaceTower() {
        Instantiate(towerPrefab, selector.transform.position, Quaternion.identity);
    }
}
