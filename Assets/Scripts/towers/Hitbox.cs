using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 1;
    public float invulnSeconds = 0.5f; // invlun time of enemy after being hit
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "enemy")  {
            Debug.Log("Take damage");
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public int GetDamage() {
        return damage;
    }

    public float GetInvulnSeconds() {
        return invulnSeconds;
    }
}
