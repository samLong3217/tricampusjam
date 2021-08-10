using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Tower, ITakesDamage
{
    public override void Update()
    {
        // do nothing. we need to overide the default tower behavior
    }
   protected override void OnRegister(bool success) {
        if (success) {
            GameObject player = GameObject.FindWithTag("Player");
            if (!player.GetComponent<PlayerController>().PlantCrop()) {
                Destroy(gameObject);
            }
        }
    }

    public override void TakeDamage(IDamager damager, float damage) {
        Destroy(gameObject);
    }
}
