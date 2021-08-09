using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float hp = 5;

    Dictionary<GameObject, bool> registeredHitboxes = new Dictionary<GameObject, bool>(); // Used to register hitboxes seen. True if in i frames

    public float Speed = 2f;
    public float DragMultiplier = 0.9f;

    private Rigidbody2D _rigidbody2d;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (hp <= 0) {
            Destroy(gameObject);
        }
        
        Vector2Int target = AIManager.GetTarget(this);
        Vector3 position = transform.position;
        Vector2 translation = new Vector2(target.x - position.x, target.y - position.y);
        _rigidbody2d.velocity *= DragMultiplier;
        _rigidbody2d.AddForce(translation.normalized * (Speed * _rigidbody2d.mass));

        if (AIManager.DebugMode)
        {
            Debug.DrawLine(position, (Vector2) target, Color.green, 0, false);
        }
    }

    public void TakeDamage(int amt)
    {
        hp -= amt;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "hitbox") {
            // Have we seen this hitbox before?
            if ((registeredHitboxes.ContainsKey(other.gameObject) && !registeredHitboxes[other.gameObject]) || !registeredHitboxes.ContainsKey(other.gameObject)) {
                Hitbox hitbox = other.gameObject.GetComponent<Hitbox>();
                TakeDamage(hitbox.GetDamage());
                // Register the hitbox
                registeredHitboxes[other.gameObject] = true;
                StartCoroutine(DisableIFrames(hitbox.GetInvulnSeconds(), other.gameObject));
            }
        }
    }

    IEnumerator DisableIFrames(float iframes, GameObject gameObject) {
        yield return new WaitForSeconds(iframes);
        Debug.Log("No longer immune");
        registeredHitboxes[gameObject] = false;
    }
}
