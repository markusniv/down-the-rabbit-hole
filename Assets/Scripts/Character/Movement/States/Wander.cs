
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Characters in this state will wander around the room randomly.
/// </summary>
public class Wander : State
{
    public Vector2 CurrentDestination { get; private set; }

    private float? CooldownTimer;

    /// <summary>
    /// Walk Timer in seconds
    /// </summary>
    private float WalkTimer;

    private Room Room;
    public Wander(Character character, Room room) : base(character)
    {
        Room = room;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.Movement = Vector2.zero;
    }

    private void SetRandomDestination()
    {
        CurrentDestination = new Vector2(Random.Range(Room.InnerBounds.min.x, Room.InnerBounds.max.x - 2), Random.Range(Room.InnerBounds.min.y, Room.InnerBounds.max.y - 2));
        WalkTimer = Vector2.Distance(CurrentDestination, Character.transform.position) / Character.Movement.MovementSpeed;
    }

    public override void OnFixedUpdate()
    {
        if(WalkTimer > 0)
        {
            Character.Movement.Movement = GetDirection;
            WalkTimer -= Time.fixedDeltaTime;
        }else
        {
            if(CooldownTimer == null)
            {
                Character.Movement.Movement = Vector2.zero;
                CooldownTimer = Random.Range(0.5f, 2f);
            }else if (CooldownTimer <= 0)
            {
                SetRandomDestination();
                CooldownTimer = null;
            }
            else
            {
                CooldownTimer -= Time.fixedDeltaTime;
            }
        }
    }

    private Vector2 GetDirection => (CurrentDestination - (Vector2)Character.transform.position).normalized;
}
