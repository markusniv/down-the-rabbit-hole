using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    float Timer = 1f;
    bool TimerStarted = false;
    Player Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        Player = player;
        Player.Movement.CurrentState = new Immobile(Player);
        Camera.main.GetComponent<Animator>().SetTrigger("StartVignetteClose");
        TimerStarted = true;
    }

    private void FixedUpdate()
    {
        if (!TimerStarted) return;
        Timer -= Time.fixedDeltaTime;

        if(Timer <= 0f)
        {
            Player.Movement.CurrentState = Player.Movement.PreviousState;
            GameController.Instance.CurrentFloor.FloorNumber++;
            GameController.Instance.CurrentFloor.CreateRooms();
        }
    }

}
