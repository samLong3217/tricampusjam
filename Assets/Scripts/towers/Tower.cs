using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Wall
{
    public float totalLife = 10.0f; // how long it takes for the tower to decay
    public int cost = 10; // cost to build the tower

    private float remainingLife; 

    public virtual void Update() {
        remainingLife -= Time.deltaTime;
        if (remainingLife <= 0) {
            Destroy(gameObject);
        }
    }

    protected override void OnRegister(bool success) {
        if (success) {
            GameObject player = GameObject.FindWithTag("Player");
            if (!player.GetComponent<PlayerController>().PayForTower(cost)) {
                Destroy(gameObject);
            }
            remainingLife = totalLife;
        } 
    }

    public override void TakeDamage(IDamager damager, float damage) {
        totalLife -= damage;
    }
}
