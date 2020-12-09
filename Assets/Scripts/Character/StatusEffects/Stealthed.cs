using UnityEngine;


/// <summary>
/// Characters with this status are in stealth and enemies cannot see then.
/// </summary>
public class Stealthed : StatusEffect
{
    private SpriteRenderer CharacterSpriteRenderer;

    /// <summary>
    /// Duration is 5 second
    /// </summary>
    private float Duration = 5f;

    public Stealthed(Character character) : base(character)
    {
        /// <summary>
        /// Gets the Sprite renderer
        /// </summary>
        CharacterSpriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// When it enter the status effect it will change the character to color to be more see trough
    /// </summary>
    public override void OnStatusEnter()
    {
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 0.2f);
        base.OnStatusEnter();
    }

    /// <summary>
    /// When it exits the status effect it will restore the original color.
    /// </summary>
    public override void OnStatusExit()
    {
        CharacterSpriteRenderer.color = new Color(1, 1, 1, 1f);
        base.OnStatusExit();
    }

    /// <summary>
    /// Removes this effect after <see cref="Duration"/> is 0
    /// </summary>
    public override void OnFixedUpdate()
    {
        // Reduces the duration with current time per second
        Duration -= Time.fixedDeltaTime;
        // if the duration is 0 or smaller it will stop the effect.
        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}