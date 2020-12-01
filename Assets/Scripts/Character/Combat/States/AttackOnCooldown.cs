using UnityEngine;

/// <summary>
/// This state happens in combat when an attack is on cooldown
/// </summary>
public class AttackOnCooldown : State
{
    /// <summary>
    /// Cooldown variable we're going to count down
    /// </summary>
    public float Cooldown;
    public AttackOnCooldown(Character character) : base(character)
    {
    }
    /// <summary>
    /// When entering this state, set the cooldown to that of the current weapon's cooldown and invoke
    /// cooldown start state as the character's combat state
    /// </summary>
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Cooldown = Character.Combat.CurrentWeapon.attackCooldownDefault;
        Character.Combat.InvokeCooldownStart(Character.Combat.CurrentWeapon);
    }
    /// <summary>
    /// When leaving this state, invoke the cooldown end state as the character's combat state
    /// </summary>
    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Combat.InvokeCooldownEnd(Character.Combat.CurrentWeapon);
    }
    /// <summary>
    /// Reduce the cooldown and once it's finished, change state to Idle
    /// </summary>
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Cooldown -= Time.fixedDeltaTime;
        if(Cooldown <= 0f)
        {
            Character.Combat.CurrentState = new Idle(Character);
        }
    }
}