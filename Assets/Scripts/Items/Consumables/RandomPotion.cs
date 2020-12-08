using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPotion : Consumable
{
    public override string Tooltip => "Mystery never know what in it till you try it.";



    public Consumable SelectedPotion;

    protected override void Awake()
    {
        GameObject[] possiblePotions = Resources.LoadAll<GameObject>("Prefabs/Items/Consumables");
        var consumables = possiblePotions.Select(x => x.GetComponent<Consumable>()).Where(x => !(x is RandomPotion));
        SelectedPotion = GameObject.Instantiate(consumables.ElementAt(Random.Range(0, consumables.Count())).gameObject).GetComponent<Consumable>();
        SelectedPotion.gameObject.SetActive(false);
        SelectedPotion.Uses = 9999;
    }

    public override void OnPickup(Character pickedUpBy)
    {
        base.OnPickup(pickedUpBy);
        SelectedPotion.Inventory = Inventory;
    }

    public override void OnDrop(Character droppedBy)
    {
        base.OnDrop(droppedBy);
        SelectedPotion.Inventory = null;
    }


    public override void Consume()
    {
        SelectedPotion.Consume();
        base.Consume();
        Destroy(SelectedPotion.gameObject);
    }
}