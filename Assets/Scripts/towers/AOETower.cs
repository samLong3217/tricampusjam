using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : Tower
{
    public float fireSpeed = 4.0f;

    public int damage = 3;

    public GameObject hitbox;

    public override void Start() {
        base.Start();
        StartCoroutine("FireTower");
    }

    IEnumerator FireTower() {
     for(;;) {
        Debug.Log("FIRE!");
        GameObject hitboxInstance =  Instantiate(hitbox, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(hitboxInstance);
         yield return new WaitForSeconds(fireSpeed);
     }
    }
}
