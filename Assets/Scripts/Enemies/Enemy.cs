using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamager
{
    public float hp = 5;

    private Dictionary<GameObject, float> registeredHitboxes = new Dictionary<GameObject, float>(); // Used to register hitboxes seen. If the value is >= 0, still invlun to that hitbox

    public float Speed = 10f;
    public float HopTime = 0.25f;
    public float AttackDamage = 1;
    public float AttackPushbackAmount = 10;
    public float AttackCooldown = 0.5f;

    public Collider2D AttackCollider;

    private float _hopCooldown = 0;
    private float _attackCooldown;
    
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
        translation = translation.normalized + Random.insideUnitCircle * 0.01f;
        
        _hopCooldown -= Time.deltaTime;
        while (_hopCooldown <= 0)
        {
            Rigidbody2d.AddForce(translation.normalized * (Speed * Rigidbody2d.mass * HopTime), ForceMode2D.Impulse);
            _hopCooldown += HopTime * (0.75f + 0.5f * Random.value);
        }
        
        transform.rotation = Quaternion.identity;

        if (_attackCooldown > 0) _attackCooldown -= Time.deltaTime;
        if (_attackCooldown <= 0)
        {
            AttackCollider.enabled = true;
        }

        if (AIManager.DebugMode)
        {
            Debug.DrawLine(position, (Vector2) target, Color.green, 0, false);
        }
    }

    public void TakeDamage(int amt)
    {
        hp -= amt;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "hitbox") {
            // Have we seen this hitbox before?
            if ((registeredHitboxes.ContainsKey(other.gameObject) && registeredHitboxes[other.gameObject] <= 0.0f) || !registeredHitboxes.ContainsKey(other.gameObject)) {
                Hitbox hitbox = other.gameObject.GetComponent<Hitbox>();
                TakeDamage(hitbox.GetDamage());
                // Register the hitbox
                registeredHitboxes[other.gameObject] = hitbox.GetInvulnSeconds();
            }
        }
        else if (other.CompareTag("takes damage"))
        {
            ITakesDamage target = other.GetComponent<ITakesDamage>();
            target.TakeDamage(this, AttackDamage);
        }
    }

    public void DealtDamage(ITakesDamage target, float damage)
    {
        MonoBehaviour targetMb = target as MonoBehaviour;
        if (targetMb != null)
        {
            Vector2 delta = targetMb.transform.position - transform.position;
            Rigidbody2d.AddForce(-delta.normalized * AttackPushbackAmount, ForceMode2D.Impulse);
            AttackCollider.enabled = false;
            _attackCooldown = AttackCooldown;
        }
    }
}
