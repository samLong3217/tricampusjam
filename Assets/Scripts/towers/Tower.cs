using UnityEngine;

public class Tower : Wall
{
    public float Lifespan = 10.0f; // how long it takes for the tower to decay
    public int cost = 10; // cost to build the tower

    private float _decayPerSecond;

    public virtual void Update() {
        TakeDamage(null, _decayPerSecond);
    }

    protected override void OnRegister(bool success) {
        if (success) {
            GameObject player = GameObject.FindWithTag("Player");
            if (!player.GetComponent<PlayerController>().PayForTower(cost)) {
                Destroy(gameObject);
            }

            _decayPerSecond = Health / Lifespan * Time.deltaTime;
        } 
    }
}
