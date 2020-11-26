using System.Linq;
using UnityEngine;

public class Enemy : Character
{

    /// <summary>
    /// Particles that will be shown when <see cref="Enemy"/> dies
    /// </summary>
    [SerializeField]
    private GameObject DeathParticles;

    /// <summary>
    /// Called when health is 0. Enemy will drop all items when it dies.
    /// </summary>
    public override void Die()
    {
        base.Die();
        foreach(Item item in Inventory.Items.ToList())
        {
            Inventory.DropItem(item);
        }
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}