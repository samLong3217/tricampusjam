using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            Destroy(this);
        }
    }

    public void TakeDamage(int amt) {
        hp -= amt;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Is this triggering enemy?");

    }
}
