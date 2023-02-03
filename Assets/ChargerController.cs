using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : StateMachineBehaviour
{
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float stopChargingDist = 10f;
    [SerializeField] float acceptableRotationError = 0.5f;

    GameObject player;
    Rigidbody rigidbody = null;

    bool initialRotrationDone = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = CharacterControls.Instance.gameObject;
        rigidbody = animator.gameObject.GetComponent<Rigidbody>();
        initialRotrationDone = false;
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player == null)
        {
            return;
        }

        if (Vector3.Distance(animator.transform.position, player.transform.position) > stopChargingDist)
		{
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
			animator.SetBool("InChargingRange", false);
		}

        if(rigidbody == null)
        {
            Debug.LogWarning("Could not get my rigid body");
            return;
        }


		Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - animator.transform.position);
		animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

		if (!initialRotrationDone)
        {
			initialRotrationDone = Quaternion.Angle(desiredRotation, animator.transform.rotation) < acceptableRotationError;
            if(initialRotrationDone)
			{
				animator.transform.rotation = desiredRotation;
			}
            return;
        }

        if(rigidbody.velocity.magnitude >= maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            return;
        }

        rigidbody.velocity = rigidbody.transform.forward * (rigidbody.velocity.magnitude + acceleration * Time.deltaTime);
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
