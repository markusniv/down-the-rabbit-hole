using UnityEngine;

/// <summary>
/// This script handles Rabbit Hole Compass. Press C To open in game
/// </summary>
public class RabbitHoleCompass : MonoBehaviour
{
    /// <summary>
    /// Rabbit holes world position
    /// </summary>
    public static Vector3 RabbitHoleLocation;

    /// <summary>
    /// Gets angle between player and rabbit hole
    /// </summary>
    private float Angle => Vector2.SignedAngle(RabbitHoleLocation - GameController.Instance.Player.transform.position, Vector2.up);

    public GameObject Pivot;

    /// <summary>
    /// Updates rotation
    /// </summary>
    private void Update()
    {
        Pivot.transform.localEulerAngles = new Vector3(0, 0, -Angle);
    }

}