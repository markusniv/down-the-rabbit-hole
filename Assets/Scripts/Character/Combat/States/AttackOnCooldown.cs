using UnityEngine;

public class AttackOnCooldown : State
{
    public float Cooldown;
    public AttackOnCooldown(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Cooldown = Character.Combat.CurrentWeapon.attackCooldownDefault;
        Character.Combat.InvokeCooldownStart(Character.Combat.CurrentWeapon);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Combat.InvokeCooldownEnd(Character.Combat.CurrentWeapon);
    }

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