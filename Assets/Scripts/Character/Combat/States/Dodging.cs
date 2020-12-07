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

    public Vector2 DodgeDestination;
    public float DodgeSpeed = 50f;
    public float DodgeDistance = 6f;
    float DodgeDuration;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Character.Movement.CurrentState = new Immobile(Character);

        DodgeDestination = CharacterCollider.bounds.center + (-DirectionToTarget) * DodgeDistance;
        DodgeDuration = DistanceToDestination / DodgeSpeed;
        Target.Combat.OnAttackEnd += RestoreState;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Target.Combat.OnAttackEnd -= RestoreState;
        Character.Movement.CurrentState = Character.Movement.PreviousState;
    }

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
