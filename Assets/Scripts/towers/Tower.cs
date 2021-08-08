using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float decayTime = 10.0f; // how long it takes for the tower to decay

    public int cost = 10; // cost to build the tower
     public virtual void Start()
    {
        StartCoroutine("Decay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Decay() {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
