
/// <summary>
/// This script manages how enemies move.
/// </summary>
public class EnemyMovement : CharacterMovement
{

    protected override float BaseMovementSpeed => 3f;

    protected override void Start()
    {
        base.Awake();
        OnRoomEnter += StartWandering;
        GameController.Instance.Player.Movement.OnRoomLeave += PlayerLost;
    }

    /// <summary>
    /// Sets movement state to wander.
    /// </summary>
    /// <param name="room"></param>
    public void StartWandering(Room room)
    {
        var followState = new FollowTarget(Character, GameController.Instance.Player);
        var lookForState = new LookForCharacter(Character, GameController.Instance.Player, room, followState);
        CurrentState = lookForState;
    }

    /// <summary>
    /// Triggered when player leaves room. This will reset state to <see cref="LookForCharacter"/>
    /// </summary>
    /// <param name="room">Room where player left</param>
    private void PlayerLost(Room room)
    {
        // Don't do anything if player didn't exit the room where this enemy was
        if (Character.Movement.CurrentRoom != room) return;
        if(Character.Combat is EnemyCombat combat)
        {
            combat.CurrentState = new Idle(Character);
        }
        StartWandering(CurrentRoom);
    }
}