using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Tower
{
    public int radius = 3;
    public float fireSpeed = 1.0f;
    public GameObject bullet;

    private float fireTime;
    private AudioSource source;

    public override void Update() {
        base.Update();
        fireTime -= Time.deltaTime;

        if (fireTime <= 0) {
            GameObject target = GetEnemyInRange();
            if (target != null) { // we found something to fire at
                GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletInstance.GetComponent<Bullet>().target = target;
                source.Play();
            }
            fireTime = fireSpeed;
        }
    }

    private GameObject GetEnemyInRange() {
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

    protected override void OnRegister(bool success) {
        base.OnRegister(success);
        if (success) {
            fireTime = fireSpeed;
            source = GetComponent<AudioSource>();
        }
    }
}
