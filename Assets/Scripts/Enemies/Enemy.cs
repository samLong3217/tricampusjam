using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float hp = 5;
    public float Speed = 0.5f;

    private Dictionary<GameObject, float> registeredHitboxes = new Dictionary<GameObject, float>(); // Used to register hitboxes seen. If the value is >= 0, still invlun to that hitbox

    private void Update()
    {
        if (hp <= 0) {
            Destroy(gameObject);
        }

        foreach (var item in registeredHitboxes) { // adjust hitbox times
            registeredHitboxes[item.Key] -= Time.deltaTime;
        }
        
        Vector2Int target = AIManager.GetTarget(this);
        Vector3 position = transform.position;
        Vector3 translation = new Vector3(target.x - position.x, target.y - position.y);
        transform.Translate(translation.normalized * (Speed * Time.deltaTime));
    }

    public void TakeDamage(int amt)
    {
        hp -= amt;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "hitbox") {
            // Have we seen this hitbox before?
            if ((registeredHitboxes.ContainsKey(other.gameObject) && registeredHitboxes[other.gameObject] <= 0.0f) || !registeredHitboxes.ContainsKey(other.gameObject)) {
                Hitbox hitbox = other.gameObject.GetComponent<Hitbox>();
                TakeDamage(hitbox.GetDamage());
                // Register the hitbox
                registeredHitboxes[other.gameObject] = hitbox.GetInvulnSeconds();
            }
        }
    }
}
