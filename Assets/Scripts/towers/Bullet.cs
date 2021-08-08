using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target = null;

    public float speed = 1.0f;

    // Update is called once per frame
    void Update() {
        if (target != null) {
             Vector3 moveDir = (target.transform.position - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "enemy" && other.gameObject == target)  {
            target.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
