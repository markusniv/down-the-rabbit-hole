using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Room;
using Random = UnityEngine.Random;

/// <summary>
/// Generates and controls rooms. Level has multiple floors, but only one is shown at the time.
/// </summary>
/// <remarks>
/// Can create and control rooms.
/// </remarks>
public class Floor : MonoBehaviour
{
    /// <summary>
    /// Here we create rooms
    /// </summary>
    private void Start()
    {
        CreateRooms();
    }

    /// <summary>
    /// Triggered at end of the <see cref="CreateRooms"/>
    /// </summary>
    public event Action<int> OnFloorCreated;

    /// <summary>
    /// Grid of rooms. Accessed by using location of the room.
    /// </summary>
    public IDictionary<Room.GridLocation, Room> RoomGrid;

    /// <summary>
    /// Current floor number
    /// </summary>
    public int FloorNumber = 1;

    public int MaxRoomCount = 15;

    /// <summary>
    /// Creates rooms under current floor. Existing rooms are destroyed if they exist
    /// </summary>
    public void CreateRooms()
    {
        DestroyRooms();
        var spawnRoom = CreateRoom<SpawnRoom>(new Room.GridLocation(0, 0), true);
        GameController.Instance.Player.transform.position = spawnRoom.Center;
        CreateSurroundingRooms(spawnRoom);
        CreateRabbitHoleRoom();
        SetDoors();
        Camera.main.GetComponent<Animator>().SetTrigger("StartVignetteOpen");
        OnFloorCreated?.Invoke(FloorNumber);
    }

    /// <summary>
    /// Destroys all child objects and clears <see cref="RoomGrid"/>
    /// </summary>
    public void DestroyRooms()
    {
        foreach (Transform room in transform)
        {
            Destroy(room.gameObject);
        }
        RoomGrid = new Dictionary<Room.GridLocation, Room>();
    }

    /// <summary>
    /// Tries to destroy room at specific location
    /// </summary>
    /// <param name="location">Location where to destroy</param>
    /// <returns>Returns true if room was destroyed</returns>
    public bool TryDestroyRoom(Room.GridLocation location)
    {
        if (RoomGrid.TryGetValue(location, out Room room))
        {
            Destroy(room.gameObject);
            RoomGrid.Remove(location);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates room and adds it to <see cref="RoomGrid"/>
    /// </summary>
    /// <param name="location">Location in the grid</param>
    /// <param name="replace">Will replace room at that location if set true</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Returns created room</returns>
    public Room CreateRoom<T>(Room.GridLocation location, bool replace = true) where T : Room
    {
        if (replace) TryDestroyRoom(location);
        if (!replace && RoomGrid.ContainsKey(location)) return null;

        var prefab = PrefabHelper.GetRoomPrefab<T>();
        var newRoom = Instantiate(prefab, transform).GetComponent<Room>();
        newRoom.Create(location);
        newRoom.Populate();
        RoomGrid.Add(location, newRoom);
        return newRoom;
    }

    /// <summary>
    /// Finds suitable room and replaces it with rabbit hole room. Floor should contain single rabbit hole
    /// </summary>
    public Room CreateRabbitHoleRoom()
    {
        var suitableRooms = Enumerable.Empty<KeyValuePair<GridLocation, Room>>();
        int roomC = 0;
        do
        {
            suitableRooms = RoomGrid.Where(x => x.Value.DoorCount() == roomC).ToList();
            roomC++;
            if (roomC >= 5) throw new Exception("Didn't find suitable room for rabbit hole");
        } while (suitableRooms.Count() < 1);

        var rabbitHole = suitableRooms.ElementAt(Random.Range(0, suitableRooms.Count()));
        var rabbitHoleRoom = CreateRoom<RabbitHoleRoom>(rabbitHole.Key);
        return rabbitHoleRoom;
    }

    /// <summary>
    /// This method sets automatically doors to all rooms.
    /// </summary>
    private void SetDoors()
    {
        foreach (var room in RoomGrid)
        {
            room.Value.SetDoors(GetSurroundingRooms(room.Value)?.Select(x => x.Key)?.Aggregate((a, b) => a | b) ?? Room.DoorLocation.None);
        }
    }

    /// <summary>
    /// Checks doors in the room and creates new rooms around accordingly. This is recursive function.
    /// </summary>
    /// <param name="room">Rooms will be created around this room.</param>
    private void CreateSurroundingRooms(Room room)
    {
        if (RoomGrid.Count > MaxRoomCount) return;
        List<Room.GridLocation> newRoomLocations = new List<Room.GridLocation>();
        // If room has top door
        if (room.Doors.HasFlag(Room.DoorLocation.Top))
        {
            newRoomLocations.Add(new Room.GridLocation(room.Location.X, room.Location.Y + 1));
        }
        // If room has right door
        if (room.Doors.HasFlag(Room.DoorLocation.Right))
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

        foreach (var location in newRoomLocations)
        {
            Room newRoom;
            if(Random.Range(0,100) <= 25)
            {
                newRoom = CreateRoom<ChestRoom>(location, false);
            }else
            {
                newRoom = CreateRoom<GenericRoom>(location, false);
            }
            if (newRoom == null) continue;
            CreateSurroundingRooms(newRoom);
        }
    }

    /// <summary>
    /// This will get all surrounding rooms. This is used to "fix" doors after floor has been created.
    /// </summary>
    /// <param name="room">Reference Room</param>
    /// <returns>Returns surrounding rooms</returns>
    private Dictionary<Room.DoorLocation, Room> GetSurroundingRooms(Room room)
    {
        var rooms = new Dictionary<Room.DoorLocation, Room>();
        if (RoomGrid.TryGetValue(new Room.GridLocation(room.Location.X, room.Location.Y + 1), out Room topRoom))
        {
            rooms.Add(Room.DoorLocation.Top, topRoom);
        }
        if (RoomGrid.TryGetValue(new Room.GridLocation(room.Location.X + 1, room.Location.Y), out Room rightRoom))
        {
            rooms.Add(Room.DoorLocation.Right, rightRoom);
        }
        if (RoomGrid.TryGetValue(new Room.GridLocation(room.Location.X, room.Location.Y - 1), out Room bottomRoom))
        {
            rooms.Add(Room.DoorLocation.Bottom, bottomRoom);
        }
        if (RoomGrid.TryGetValue(new Room.GridLocation(room.Location.X - 1, room.Location.Y), out Room leftRoom))
        {
            rooms.Add(Room.DoorLocation.Left, leftRoom);
        }
        return rooms.Count > 0 ? rooms : null;
    }
}