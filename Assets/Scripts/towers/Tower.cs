using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float totalLife = 10.0f; // how long it takes for the tower to decay
    float remainingLife; 

    public int cost = 10; // cost to build the tower
     public virtual void Start() {
        remainingLife = totalLife;
    }

    // Update is called once per frame
    public virtual void Update() {
        remainingLife -= Time.deltaTime;
        if (remainingLife <= 0) {
            Destroy(gameObject);
        }
    }
}
