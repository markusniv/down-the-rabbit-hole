using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : StatusEffect
{
    float Duration = 5f;

    float DamagePerTick = 100f;

    public Poisoned(Character character) : base(character)
    {
    }


    public override void OnFixedUpdate()
    {
        Duration -= Time.fixedDeltaTime;

        Character.CurrentHealth -= DamagePerTick * Time.fixedDeltaTime;

        if(Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}
