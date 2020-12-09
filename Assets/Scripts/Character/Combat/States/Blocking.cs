
using UnityEngine;
/// <summary>
/// Characters with this state will block any weapon damage.
/// </summary>
public class Blocking : State
{
    /// <summary>
    /// Duration of the block in seconds. Default 0.5 seconds
    /// </summary>
    public float Duration = 0.5f;

    public Blocking(Character character, float duration = 0.5f) : base(character)
    {
        Duration = duration;
    }

    /// <summary>
    /// Immobilize character, show weapon without collider and do block animation
    /// </summary>
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Character.Movement.CurrentState = new Immobile(Character);
        Character.Combat.CurrentWeapon?.Show(true);
        Character.Combat.CurrentWeapon?.Block();
    }

    /// <summary>
    /// Restore movemement state and hide the weapon
    /// </summary>
    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        Character.Combat.CurrentWeapon?.Hide();
    }

    /// <summary>
    /// Called on successful block. Plays deflect sound
    /// </summary>
    public void OnHit()
    {
        SoundManagerScript.PlaySound(SoundManagerScript.Sound.Deflect);
    }


    /// <summary>
    /// Calculates when this state should end
    /// </summary>
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Duration -= Time.fixedDeltaTime;
        if(Duration <= 0f)
        {
            Character.Combat.CurrentState = new Idle(Character);
        }
    }

}