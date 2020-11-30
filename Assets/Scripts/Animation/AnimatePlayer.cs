using UnityEngine;

public class AnimatePlayer : Animate
{

    public override void Update()
    {
        if (CharacterMovement.Movement == Vector2.zero) return;
        base.Update();
    }
}