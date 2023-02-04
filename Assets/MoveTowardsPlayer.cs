using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : StateMachineBehaviour
{
    [SerializeField] float breakDist = 2f;
    [SerializeField] float speed = 4f;
    GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(CharacterControls.Instance == null) 
        {
            player = null;
            return;
        }

        player = CharacterControls.Instance.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player == null) { return; }

        animator.transform.position = Vector3.MoveTowards(animator.transform.position, player.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(animator.transform.position, player.transform.position) > breakDist)
		{
			animator.SetBool("InChargingRange", false);
		}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
