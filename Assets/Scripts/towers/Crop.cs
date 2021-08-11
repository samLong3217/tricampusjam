using UnityEngine;

public class Crop : Wall, ITakesDamage
{
    protected override void Start()
    {
        Vector3 position = transform.position;
        Location = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        if (!TargetManager.Register(this))
        {
            OnRegister(false);
            Destroy(gameObject);
        }
        else
        {
            OnRegister(true);
        }
    }

    protected override void OnRegister(bool success)
    {
        if (success)
        {
            RoundManager.IncrementCrops();
        }
    }

    public override void TakeDamage(IDamager damager, float damage)
    {
        base.TakeDamage(damager, damage);
        Debug.Log("Is this called?");
        RoundManager.DecrementCrops();
        Destroy(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        TargetManager.Unregister(this);
    }

    public override void Sell()
    {
        base.Sell();
        RoundManager.DecrementCrops();
    }
}
