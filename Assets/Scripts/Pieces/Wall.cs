public class Wall : GridObject, ITakesDamage
{
    public float Health = 30;

    public override IPathfindingNode GetPathfindingNode()
    {
        return new BasicPathfindingNode(Location, Health * 2 + 1);
    }

    public virtual void TakeDamage(IDamager damager, float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        
        damager.DealtDamage(this, damage);
        
        print("Ouch: " + Health);
    }
}
