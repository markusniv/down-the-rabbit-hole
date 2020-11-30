
using UnityEngine;
/// <summary>
/// Characters in this state will follow specified character.
/// </summary>
public class FollowTarget : State
{
    public Character Target { get; set; }

    public float FollowDistance = 2f;

    public bool AtRange { get; private set; }

    CircleCollider2D CharacterCollider;
    CircleCollider2D TargetCollider;

    public FollowTarget(Character character, Character target) : base(character)
    {
        Target = target;
        CharacterCollider = character.GetComponent<CircleCollider2D>();
        TargetCollider = character.GetComponent<CircleCollider2D>();
    }

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
