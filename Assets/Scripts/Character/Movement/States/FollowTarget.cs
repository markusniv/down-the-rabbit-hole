
using UnityEngine;
/// <summary>
/// Characters in this state will follow specified character.
/// </summary>
public class FollowTarget : State
{
    /// <summary>
    /// Specifies which target to follow
    /// </summary>
    public Character Target { get; set; }

    /// <summary>
    /// Character won't get closer than this
    /// </summary>
    public float FollowDistance = 2f;

    /// <summary>
    /// Is True when distance between character and target is equal to <see cref="FollowDistance"/>
    /// </summary>
    public bool AtRange { get; private set; }

    CircleCollider2D CharacterCollider;
    CircleCollider2D TargetCollider;

    public FollowTarget(Character character, Character target) : base(character)
    {
        Target = target;
        CharacterCollider = character.GetComponent<CircleCollider2D>();
        TargetCollider = target.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Moves closer to target when not in range and stands still if in range
    /// </summary>
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if(DistanceToTarget > FollowDistance)
        {
            AtRange = false;
            Character.Movement.Movement = DirectionToTarget;
        }else
        {
            AtRange = true;
            Character.Movement.Movement = Vector2.zero;
        }
    }

    private Vector3 DirectionToTarget => (TargetCollider.bounds.center - CharacterCollider.bounds.center).normalized;
    private float DistanceToTarget => Vector3.Distance(TargetCollider.bounds.center, CharacterCollider.bounds.center);
}
