using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : Tower
{
    public float fireSpeed = 4.0f;

    public int damage = 3;

    public GameObject hitbox;

    private float fireTime;
    private bool fired; // you're fired
    private GameObject hitboxInstance;

    public override void Start() {
        base.Start();
        fireTime = fireSpeed;
    }

    public override void Update() {
        base.Update();
        fireTime -= Time.deltaTime;
        if (fireTime <= 0 && !fired) {
            hitboxInstance =  Instantiate(hitbox, transform.position, Quaternion.identity);
            fired = true;
            Debug.Log("Fire");
        } else if (fireTime <= -0.2f && fired) {
            if (hitboxInstance != null) {
                Destroy(hitboxInstance);
                Debug.Log("destroyed hitbox");
            }
            fired = false;
            fireTime = fireSpeed;
        }
    }
}
