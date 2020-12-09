using System.Linq;
using UnityEngine;

/// <summary>
/// Potion that will teleport the player into random room
/// </summary>
public class TeleportPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => "Target teleported to a another room";

    /// <summary>
    /// Consumes the item and teleports character to random room
    /// </summary>
    public override void Consume()
    {
        // Gets the game object with tag

        var controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Set a random number via current floor count

        var key = controller.CurrentFloor.RoomGrid.Keys.ToList().ElementAt(Random.Range(0, controller.CurrentFloor.RoomGrid.Count - 1));

        // Sets the current value of key to the current room to switch.

        Inventory.transform.position = controller.CurrentFloor.RoomGrid[key].Center;

        base.Consume();
    }
}