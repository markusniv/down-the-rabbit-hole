using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealthed : StatusEffect
{
    SpriteRenderer CharacterSpriteRenderer;

    float Duration = 5f;

    public Stealthed(Character character) : base(character)
    {
        CharacterSpriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    public override void OnStatusEnter()
    {
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 0.2f);
        base.OnStatusEnter();
    }

    public override void OnStatusExit()
    {
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 1f);
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
