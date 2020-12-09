using System.Linq;
using UnityEngine;

/// <summary>
/// Characters in this state will look for specified character
/// </summary>
public class LookForCharacter : Wander
{

    /// <summary>
    /// Target who to look for
    /// </summary>
    public Character Target;

    /// <summary>
    /// How far this character can detect target
    /// </summary>
    public float DetectRadius = 5f;

    /// <summary>
    /// Character enters this state when it detects <see cref="Target"/>
    /// </summary>
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

    /// <summary>
    /// Tries to find <see cref="Target"/>. Does raycast around the character and if it hits <see cref="Target"/> this character will move to the next state
    /// </summary>
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Target.Movement.CurrentRoom != Character.Movement.CurrentRoom) return;
        var raycastHit = Physics2D.Raycast(CharacterCollider.bounds.center, DirectionToTarget, DetectRadius, LayerMask.GetMask("Characters", "VisionBlock"));
        Debug.DrawLine(CharacterCollider.bounds.center, CharacterCollider.bounds.center + DirectionToTarget * DetectRadius);
        if(raycastHit.collider != null && raycastHit.collider.TryGetComponent(out Character character) && character == Target)
        {
            if (character.StatusEffects.Any(x => x is Stealthed)) return;

            if (NextState != null) Character.Movement.CurrentState = NextState;
        }
    }

    private Vector3 DirectionToTarget => (TargetCollider.bounds.center - CharacterCollider.bounds.center).normalized;
}
