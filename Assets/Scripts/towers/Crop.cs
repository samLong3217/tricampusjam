using UnityEngine;

public class Crop : Tower, ITakesDamage
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
    
    public override void Update()
    {
        // do nothing. we need to overide the default tower behavior
    }

    protected override void OnRegister(bool success)
    {
        if (success)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (!player.GetComponent<PlayerController>().PlantCrop())
            {
                Destroy(gameObject);
            }
            else
            {
                RoundManager.incrementCrops();
            }
        }
    }

    public override void TakeDamage(IDamager damager, float damage)
    {
        base.TakeDamage(damager, damage);
        Debug.Log("Is this called?");
        RoundManager.decrementCrops();
        Destroy(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        TargetManager.Unregister(this);
    }
}
