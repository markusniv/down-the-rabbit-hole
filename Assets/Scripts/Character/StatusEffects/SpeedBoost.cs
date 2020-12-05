using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : StatusEffect
{
    float SpeedChange = 2f;

    float Duration = 5f;

    public SpeedBoost(Character character, float speedChange = 2f) : base(character)
    {
        SpeedChange = speedChange;
    }

    public override void OnStatusEnter()
    {
        Character.Movement.MovementSpeedModifier += SpeedChange;
        base.OnStatusEnter();
    }

    public override void OnStatusExit()
    {
        Character.Movement.MovementSpeedModifier -= SpeedChange;
        base.OnStatusExit();
    }

    public override void OnFixedUpdate()
    {
        Duration -= Time.fixedDeltaTime;
        if(Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}
