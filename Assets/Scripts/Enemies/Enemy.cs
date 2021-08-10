using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float hp = 5;

    private Dictionary<GameObject, float> registeredHitboxes = new Dictionary<GameObject, float>(); // Used to register hitboxes seen. If the value is >= 0, still invlun to that hitbox

    public float Speed = 10f;
    public float HopTime = 0.25f;

    private float _hopCooldown = 0;
    
    public Rigidbody2D Rigidbody2d;
    
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
        Vector2 translation = new Vector2(target.x - position.x, target.y - position.y);
        
        _hopCooldown -= Time.deltaTime;
        while (_hopCooldown <= 0)
        {
            Rigidbody2d.AddForce(translation.normalized * (Speed * Rigidbody2d.mass * HopTime), ForceMode2D.Impulse);
            _hopCooldown += HopTime * (0.75f + 0.5f * Random.value);
        }
        transform.rotation = Quaternion.identity;

        if (AIManager.DebugMode)
        {
            Debug.DrawLine(position, (Vector2) target, Color.green, 0, false);
        }
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
