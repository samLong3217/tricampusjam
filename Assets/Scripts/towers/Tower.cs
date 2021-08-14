using UnityEngine;

public class Tower : Wall
{
    public float Lifespan = 10.0f; // how long it takes for the tower to decay

    private float _decayPerSecond;
    private SpriteRenderer _spriteRenderer;
    private float _InitLife;

    public virtual void Update() {
        TakeDamage(null, _decayPerSecond);

        if (_spriteRenderer != null) {
            float lifePercent = Health / _InitLife;
            float addedColor = lifePercent;
            Color goalColor = new Color(addedColor, addedColor, addedColor, 255);
            _spriteRenderer.color = goalColor;
        }
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
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _decayPerSecond = Health / Lifespan * Time.deltaTime;
            _InitLife = Health;
        } 
    }
}
