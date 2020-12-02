using UnityEngine;
using System.Linq;

public class AnimateEnemy : Animate
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (CharacterMovement.Movement == Vector2.zero) return;
        base.Update();
    }
}