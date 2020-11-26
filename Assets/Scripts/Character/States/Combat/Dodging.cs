using UnityEngine;

/// <summary>
/// Dodging state. Characters will dodge away from target while in this state.
/// </summary>
public class Dodging : State
{
    #region Components
    Rigidbody2D Rigidbody2D;
    CircleCollider2D CharacterCollider;
    CircleCollider2D TargetCollider;
    #endregion

    public Dodging(Character character, Character dodgeFrom) : base(character)
    {
        Rigidbody2D = character.GetComponent<Rigidbody2D>();
        CharacterCollider = character.GetComponent<CircleCollider2D>();
        TargetCollider = dodgeFrom.GetComponent<CircleCollider2D>();
    }

    public State PreviousMovementState;
    public Vector2 DodgeDestination;
    public float DodgeSpeed = 50f;
    public float DodgeDistance = 5f;
    float DodgeDuration;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        PreviousMovementState = Character.Movement.CurrentState;
        Character.Movement.CurrentState = new Immobile(Character);

        DodgeDestination = CharacterCollider.bounds.center + (-DirectionToTarget) * DodgeDistance;
        DodgeDuration = DistanceToDestination / DodgeSpeed;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.CurrentState = PreviousMovementState;
    }
    public override void OnUpdate()
    {
        DodgeDuration -= Time.deltaTime;
        if(DodgeDuration > 0f)
        {
            Rigidbody2D.MovePosition(Rigidbody2D.position + (DirectionToDestination * DodgeSpeed * Time.deltaTime));
        }else
        {
            Character.Combat.CurrentWeapon = new Idle(Character);
        }
    }

    private Vector3 DirectionToTarget => (TargetCollider.bounds.center - CharacterCollider.bounds.center).normalized;
    private Vector2 DirectionToDestination => (DodgeDestination - (Vector2)CharacterCollider.bounds.center).normalized;
    private float DistanceToDestination => Vector3.Distance(DodgeDestination, CharacterCollider.bounds.center);
}