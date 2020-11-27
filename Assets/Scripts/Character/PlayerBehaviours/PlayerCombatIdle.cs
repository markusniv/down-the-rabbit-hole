using UnityEngine;

public class PlayerCombatIdle : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("StartAttack");
        }
    }
}
