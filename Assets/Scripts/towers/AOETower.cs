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
    private AudioSource source;
    private Animator animator;
    public override void Update() {
        base.Update();
        fireTime -= Time.deltaTime;
        if (fireTime <= 0 && !fired) {
            hitboxInstance =  Instantiate(hitbox, transform.position, Quaternion.identity);
            fired = true;
            Debug.Log("Fire");
            source.Play();
            animator.Play("Base Layer.scythe", -1, 0f);
        } else if (fireTime <= -0.2f && fired) {
            if (hitboxInstance != null) {
                Destroy(hitboxInstance);
                Debug.Log("destroyed hitbox");
            }
            fired = false;
            fireTime = fireSpeed;
        }
    }

    protected override void OnRegister(bool success) {
        base.OnRegister(success);
        if (success) {
            fireTime = fireSpeed;
            source = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
    }
}
