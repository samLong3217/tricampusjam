using UnityEngine;

public class Tower : Wall
{
    public float Lifespan = 10.0f; // how long it takes for the tower to decay

    private float _decayPerSecond;

    public virtual void Update() {
        TakeDamage(null, _decayPerSecond);
    }

    public virtual int GetCost()
    {
        return 10;
    }

    protected override void OnRegister(bool success) {
        if (success) {
            if (!MoneyManager.Spend(GetCost()))
            {
                Destroy(gameObject);
                return;
            }
            
            _decayPerSecond = Health / Lifespan * Time.deltaTime;
        } 
    }
}
