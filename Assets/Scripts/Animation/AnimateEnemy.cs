using UnityEngine;
using System.Linq;

public class AnimateEnemy : Animate
{

    protected override Vector2 LookDirection
    {
        get
        {
            if(Character.Movement.CurrentState is FollowTarget follow)
            {
                return ((Vector2)Character.transform.position.GetDirectionTo(follow.Target.transform.position)).To4WayDirection();
            }else
            {
                return base.LookDirection;
            }

        }
    }

    public override void Start()
    {
        base.Start();
    }

}