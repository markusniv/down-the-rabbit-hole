using UnityEngine;

/// <summary>
/// Characters in this state will look for specified character
/// </summary>
public class LookForCharacter : Wander
{
    public Character Target;

    public float DetectRadius = 5f;

    public State NextState;

    Collider2D CharacterCollider;
    Collider2D TargetCollider;

    public LookForCharacter(Character character, Character target, Room room, State nextState = null) : base(character, room)
    {
        Target = target;
        NextState = nextState;
        CharacterCollider = character.GetComponent<Collider2D>();
        TargetCollider = target.GetComponent<Collider2D>();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Target.Movement.CurrentRoom != Character.Movement.CurrentRoom) return;
        var raycastHit = Physics2D.Raycast(CharacterCollider.bounds.center, DirectionToTarget, DetectRadius, LayerMask.GetMask("Characters", "VisionBlock"));
        Debug.DrawLine(CharacterCollider.bounds.center, CharacterCollider.bounds.center + DirectionToTarget * DetectRadius);
        if(raycastHit.collider != null && raycastHit.collider.TryGetComponent(out Character character) && character == Target)
        {
            if (NextState != null) Character.Movement.CurrentState = NextState;
        }
    }

    private Vector3 DirectionToTarget => (TargetCollider.bounds.center - CharacterCollider.bounds.center).normalized;
}
