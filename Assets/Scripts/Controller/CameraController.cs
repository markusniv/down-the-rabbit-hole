using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// This script controls camera movement. By Default it will follow player as they move through the doors
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Camera will follow this location
    /// </summary>
    public Vector2 Target;
    /// <summary>
    /// This specifies how fast the camera will move
    /// </summary>
    public float SmoothingTime = 0.3f;

    /// <summary>
    /// Current camera velocity
    /// </summary>
    public Vector2 Velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<PostProcessVolume>().weight = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().OnRoomEnter += OnPlayerRoomEnter;
    }
    /// <summary>
    /// Called when player enters some room
    /// </summary>
    /// <param name="room">Room where they entered</param>
    void OnPlayerRoomEnter(Room room)
    {
        Target = room.Center;
    }

    /// <summary>
    /// Smoothly moves camera to the target position
    /// </summary>
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Target, ref Velocity, SmoothingTime);

    }
}
