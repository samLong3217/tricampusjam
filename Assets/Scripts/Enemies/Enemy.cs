using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 5;
    public float Speed = 0.5f;
    
    private void Update()
    {
        if (hp <= 0) {
            Destroy(gameObject);
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Is this triggering enemy?");
    }
}
