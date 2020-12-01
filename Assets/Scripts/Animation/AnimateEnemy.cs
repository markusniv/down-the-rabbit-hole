using UnityEngine;
using System.Linq;

public class AnimateEnemy : Animate
{

    public override void Start()
    {
        base.Start();
    }

    protected override float Angle
    {
        get
        {
            Vector2 to;
            Vector2 from;
            if(Character.Movement?.CurrentState is FollowTarget following)
            {
                to = (Vector2)following.Target.gameObject.transform.position;
                from = (Vector2)transform.position;
            }else
            {
                to = CharacterMovement.Movement;
                from = Vector2.zero;
            }
            return Mathf.Round(Mathf.Atan2(to.y - from.y, to.x - from.x) * 180 / Mathf.PI / 90);
        }
    }

    public override void Update()
    {
        if (CharacterMovement.Movement == Vector2.zero) return;
        base.Update();
    }
}