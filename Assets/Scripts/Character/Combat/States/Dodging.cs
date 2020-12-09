using UnityEngine;

/// <summary>
/// Character in this state dodges away from specified character. They cannot perform other action while dodging.
/// </summary>
public class Dodging : State
{

    #region Components
    Character Target;
    Rigidbody2D CharacterRigidBody;
    CircleCollider2D CharacterCollider;
    CircleCollider2D TargetCollider;
    #endregion

    public Dodging(Character character, Character DodgeFrom) : base(character)
    {
        CharacterRigidBody = character.GetComponent<Rigidbody2D>();
        CharacterCollider = character.GetComponent<CircleCollider2D>();
        TargetCollider = DodgeFrom.GetComponent<CircleCollider2D>();
        Target = DodgeFrom;
    }

    /// <summary>
    /// Destination where this character will move to during dodge
    /// </summary>
    public Vector2 DodgeDestination;
    /// <summary>
    /// Dodge speed. Shoud be quite high so dodge is fast.
    /// </summary>
    public float DodgeSpeed = 50f;
    /// <summary>
    /// how far away from target should this character dodge.
    /// </summary>
    public float DodgeDistance = 6f;

    /// <summary>
    /// Timer how long we should move.
    /// </summary>
    float DodgeDuration;


    /// <summary>
    /// Immobilizes character, sets dodge destination and duration. Adds listener for <see cref="CharacterCombat.OnAttackEnd"/> which ends the dodge after attack has ended.
    /// </summary>
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Character.Movement.CurrentState = new Immobile(Character);

        DodgeDestination = CharacterCollider.bounds.center + (-DirectionToTarget) * DodgeDistance;
        DodgeDuration = DistanceToDestination / DodgeSpeed;
        Target.Combat.OnAttackEnd += RestoreState;
    }

    /// <summary>
    /// Removes listener and restores movement state.
    /// </summary>
    public override void OnStateExit()
    {
        base.OnStateExit();
        Target.Combat.OnAttackEnd -= RestoreState;
        Character.Movement.CurrentState = Character.Movement.PreviousState;
    }

    /// <summary>
    /// Restores state when attack ends
    /// </summary>
    /// <param name="weapon">What weapon was used to attack</param>
    private void RestoreState(Weapon weapon)
    {
        Character.Combat.CurrentState = new Idle(Character);
    }

    public override void OnUpdate()
    {
        DodgeDuration -= Time.deltaTime;
        if (DodgeDuration > 0f)
        {
            CharacterRigidBody.MovePosition(CharacterRigidBody.position + (DirectionToDestination * DodgeSpeed * Time.deltaTime));
        }
    }

    private Vector3 DirectionToTarget => (TargetCollider.bounds.center - CharacterCollider.bounds.center).normalized;
    private Vector2 DirectionToDestination => (DodgeDestination - (Vector2)CharacterCollider.bounds.center).normalized;
    private float DistanceToDestination => Vector3.Distance(DodgeDestination, CharacterCollider.bounds.center);
}
