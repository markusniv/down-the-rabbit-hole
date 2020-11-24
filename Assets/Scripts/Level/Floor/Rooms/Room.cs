using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Abstract definition of room. 
/// </summary>
public abstract class Room : MonoBehaviour
{
    /// <summary>
    /// Maximum number of doors in this room. Use this to limit door count.
    /// </summary>
    public int MaxDoors = 4;

    /// <summary>
    /// Minimum number of doors in this room. Use this to force more doors.
    /// </summary>
    public int MinDoors = 1;

    /// <summary>
    /// Room constructor.
    /// </summary>
    /// <param name="location">Location of the room (doesn't actually define location, it's just reference to it)</param>
    public Room(GridLocation location)
    {
        Location = location;
    }

    /// <summary>
    /// Location of the room relative to its room.
    /// </summary>
    [Flags]
    public enum DoorLocation
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }

    /// <summary>
    /// Door locations as bit field
    /// </summary>
    /// <example>
    /// Doors = DoorLocation.Top | DoorLocation.Bottom
    /// </example>
    public DoorLocation Doors { get; private set; }

    /// <summary>
    /// Location of the room relative to center of the grid. Center is (0,0) and room next to it is (1,0)
    /// </summary>
    public GridLocation Location { get; private set; }

    /// <summary>
    /// Derived classes may populate room with enemies or items with this method.
    /// </summary>
    public virtual void Populate() { }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Create()
    {

    }

    #region Components
    /// <summary>
    /// Inner collider that is used to detect characters entering the room.
    /// </summary>
    protected BoxCollider2D Collider;
    #endregion

    /// <summary>
    /// Virtual Awake. This initializes components. Derived classes should call this.
    /// </summary>
    protected virtual void Awake()
    {
        
    }

    /// <summary>
    /// Virtual start. Derived classes can override this.
    /// </summary>
    protected virtual void Start()
    {
    }

    /// <summary>
    /// Virtual up. Derived classes can override this.
    /// </summary>
    protected virtual void Update()
    {
    }

    /// <summary>
    /// Definition for room location. Location is defined as grid. Center is (0,0) and room next to it is (1,0).
    /// </summary>
    public struct GridLocation
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
