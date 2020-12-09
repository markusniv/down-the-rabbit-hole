using UnityEngine;

/// <summary>
/// Script that manages Rabbit hole
/// </summary>
public class RabbitHole : MonoBehaviour
{

    public void Start()
    {
        RabbitHoleCompass.RabbitHoleLocation = transform.position;
    }
    /// <summary>
    /// Transition time
    /// </summary>
    private float Timer = 1f;
    /// <summary>
    /// Transition has started
    /// </summary>
    private bool TimerStarted = false;
    /// <summary>
    /// Reference to player
    /// </summary>
    private Player Player;

    /// <summary>
    /// Checks if player collided with rabbit hole. Starts transition if so.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        Player = player;
        Player.Movement.CurrentState = new Immobile(Player);
        Camera.main.GetComponent<Animator>().SetTrigger("StartVignetteClose");
        TimerStarted = true;
    }

    /// <summary>
    /// Creates new floor when timer runs out
    /// </summary>
    private void FixedUpdate()
    {
        if (!TimerStarted) return;
        Timer -= Time.fixedDeltaTime;

        if (Timer <= 0f)
        {
            Player.Movement.CurrentState = Player.Movement.PreviousState;
            GameController.Instance.CurrentFloor.FloorNumber++;
            GameController.Instance.CurrentFloor.CreateRooms();
        }
    }
}