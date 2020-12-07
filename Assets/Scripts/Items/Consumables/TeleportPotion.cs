using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TeleportPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    
    public override string Tooltip => "Target teleported to a another room";

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
