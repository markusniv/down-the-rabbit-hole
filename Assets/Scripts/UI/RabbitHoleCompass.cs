using UnityEngine;

/// <summary>
/// This script handles Rabbit Hole Compass. Press C To open in game
/// </summary>
public class RabbitHoleCompass : MonoBehaviour
{
    public static Vector3 RabbitHoleLocation;

    private float Angle => Vector2.SignedAngle(RabbitHoleLocation - GameController.Instance.Player.transform.position, Vector2.up);

    public GameObject Pivot;

    private void Update()
    {
        Pivot.transform.localEulerAngles = new Vector3(0, 0, -Angle);
    }

}