using System.Linq;
using UnityEngine;

/// <summary>
/// Potion that will have random effects
/// </summary>
public class RandomPotion : Consumable
{
    public override string Tooltip => "Mystery never know what in it till you try it.";

    /// <summary>
    /// Selected potion to be used
    /// </summary>
    public Consumable SelectedPotion;

    /// <summary>
    /// Selects random potion
    /// </summary>
    protected override void Awake()
    {
        GameObject[] possiblePotions = Resources.LoadAll<GameObject>("Prefabs/Items/Consumables");
        var consumables = possiblePotions.Select(x => x.GetComponent<Consumable>()).Where(x => !(x is RandomPotion));
        SelectedPotion = GameObject.Instantiate(consumables.ElementAt(Random.Range(0, consumables.Count())).gameObject).GetComponent<Consumable>();
        SelectedPotion.gameObject.SetActive(false);
        SelectedPotion.Uses = 9999;
    }

    /// <summary>
    /// Sets Inventory on pickup
    /// </summary>
    /// <param name="pickedUpBy"></param>
    public override void OnPickup(Character pickedUpBy)
    {
        base.OnPickup(pickedUpBy);
        SelectedPotion.Inventory = Inventory;
    }

    /// <summary>
    /// Sets inventory to null on drop
    /// </summary>
    /// <param name="droppedBy"></param>

    public override void OnDrop(Character droppedBy)
    {
        base.OnDrop(droppedBy);
        SelectedPotion.Inventory = null;
    }

    /// <summary>
    /// Consumes <see cref="SelectedPotion"/> and destroys it
    /// </summary>
    public override void Consume()
    {
        SelectedPotion.Consume();
        base.Consume();
        Destroy(SelectedPotion.gameObject);
    }
}