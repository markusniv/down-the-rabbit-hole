using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Generates and controls rooms. Level has multiple floors, but only one is shown at the time.
/// </summary>
/// <remarks>
/// Can create and control rooms.
/// </remarks>
public class Floor : MonoBehaviour
{

    /// <summary>
    /// Grid of rooms. Accessed by using location of the room.
    /// </summary>
    public IDictionary<Room.GridLocation, Room> RoomGrid;

    /// <summary>
    /// Current floor number
    /// </summary>
    public int FloorNumber = 0;

    /// <summary>
    /// Creates rooms under current floor. Existing rooms are destroyed if they exist
    /// </summary>
    public void CreateRooms()
    {

    }

    /// <summary>
    /// Checks doors in the room and creates new rooms around accordingly. This is recursive function.
    /// </summary>
    /// <param name="room">Rooms will be created around this room.</param>
    private void CreateSurroundingRooms(Room room)
    {
        List<Room.GridLocation> newRoomLocations = new List<Room.GridLocation>();
        // If room has top door
        if(room.Doors.HasFlag(Room.DoorLocation.Top))
        {
            newRoomLocations.Add(new Room.GridLocation(room.Location.X, room.Location.Y + 1));
        }
        // If room has right door
        if(room.Doors.HasFlag(Room.DoorLocation.Right))
        {
            newRoomLocations.Add(new Room.GridLocation(room.Location.X + 1, room.Location.Y));
        }
        // If room has bottom door
        if (room.Doors.HasFlag(Room.DoorLocation.Bottom))
        {
            newRoomLocations.Add(new Room.GridLocation(room.Location.X, room.Location.Y - 1));
        }
        // If room has left door
        if (room.Doors.HasFlag(Room.DoorLocation.Left))
        {
            newRoomLocations.Add(new Room.GridLocation(room.Location.X - 1, room.Location.Y));
        }

        foreach(var location in newRoomLocations)
        {
            // Do no recreate room if it already exists
            if (RoomGrid.Keys.Contains(location)) continue;

            var prefab = GetPrefab(typeof(GenericRoom));
            var newRoom = Instantiate(prefab, transform).GetComponent<Room>();
            newRoom.Create();
        }
    }

    /// <summary>
    /// Returns prefab
    /// </summary>
    /// <param name="roomType">Room type</param>
    /// <returns>Unity Prefab</returns>
    private GameObject GetPrefab(Type roomType) => ((RoomPrefabAttribute)(roomType.GetCustomAttributes(typeof(RoomPrefabAttribute)).First())).GetPrefab(roomType);
}
