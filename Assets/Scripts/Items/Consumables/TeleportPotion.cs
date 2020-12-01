using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TeleportPotion : Consumable
{
    public override string Tooltip => "Teleports you random room";

    public override void Consume()
    {
        /// <summary>
        /// Gets the game object with tag 
        /// </summary>
        var controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        /// <summary>
        /// Set a random number via current floor count
        /// </summary>
        var key = controller.CurrentFloor.RoomGrid.Keys.ToList().ElementAt(Random.Range(0, controller.CurrentFloor.RoomGrid.Count - 1));
        /// <summary>
        /// Sets the current value of key to the current room to switch.
        /// </summary>
        Inventory.transform.position = controller.CurrentFloor.RoomGrid[key].Center;

        base.Consume();
    }
}
