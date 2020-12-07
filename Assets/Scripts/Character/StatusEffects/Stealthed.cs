using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealthed : StatusEffect
{ 
   /// <summary>
  /// 
  /// </summary>
    SpriteRenderer CharacterSpriteRenderer;

    /// <summary>
    /// Duration is 5 second
    /// </summary>
    float Duration = 5f;

    public Stealthed(Character character) : base(character)
    {
        /// <summary>
        /// Gets the Sprite renderer
        /// </summary>
        CharacterSpriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    public override void OnStatusEnter()
    {
        /// <summary>
        /// When it enter the status effect it will change the character to color to be more see trough
        /// </summary>
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 0.2f);
        base.OnStatusEnter();
    }

    public override void OnStatusExit()
    {
        /// <summary>
        /// When it exits the status effect it will restore the original color.
        /// </summary>
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 1f);
        base.OnStatusExit();
    }

    public override void OnFixedUpdate()
    {    
        /// <summary>
        /// Reduces the duration with current time per second 
        /// </summary>
        Duration -= Time.fixedDeltaTime;
        /// <summary>
        /// if the duration is 0 or smaller it will stop the effect.
        /// </summary>
        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}
