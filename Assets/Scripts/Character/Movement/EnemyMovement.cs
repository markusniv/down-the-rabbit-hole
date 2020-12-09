
/// <summary>
/// This script manages how enemies move.
/// </summary>
public class EnemyMovement : CharacterMovement
{

    protected override float BaseMovementSpeed => 3f;

    /// <summary>
    /// Setup event listeners for Player OnRoomLeave. Start wandering state.
    /// </summary>
    protected override void Start()
    {
        base.Awake();
        OnRoomEnter += StartWandering;
        StartWandering(CurrentRoom);
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
        Character.CurrentHealth = Character.MaxHealth;
        if(Character.Combat is EnemyCombat combat)
        {
            combat.CurrentState = new Idle(Character);
        }
        StartWandering(CurrentRoom);
    }
}