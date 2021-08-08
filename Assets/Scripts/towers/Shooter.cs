using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Tower
{
    public int radius = 3;

    public float fireSpeed = 1.0f;

    public GameObject bullet;

    public override void Start() {
        base.Start();
         StartCoroutine("FireTower");
    }


    IEnumerator FireTower() {
     for(;;) {
         Debug.Log("in fire");
         GameObject target = GetEnemyInRange();
         if (target != null) { // we found something to fire at
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInstance.GetComponent<Bullet>().target = target;
            Debug.Log("fire!");
         }
         yield return new WaitForSeconds(fireSpeed);
     }
    }

    GameObject GetEnemyInRange() {
        GameObject closestEnemy = null;
        float bestDistance = float.MaxValue;
        // iterate through all enemies in range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= radius && distance < bestDistance) {
                closestEnemy = enemy;
                bestDistance = distance;
            }
        }
        return closestEnemy;
    }
}
