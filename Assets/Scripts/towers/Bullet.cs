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
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Is this triggering?");
        if (other.tag == "Enemy" && other.gameObject == target)  {
            target.GetComponent<Enemy>().TakeDamage(1);
            Destroy(this);
        }
    }
}
