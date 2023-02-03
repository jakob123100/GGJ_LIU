using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PathFindToPlayer : StateMachineBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] int stopRange = 0;
    [SerializeField] float acceptableGoalError = 2f;
    [SerializeField] float rotationSpeed = 150f;

	Cell goalCell = null;
    Cell[,] grid;
    Cell[] path;
    int pathIndex = -1;
    float closeEnough = 0.2f;

    private async void findPath(Animator animator)
    {
		grid = Grid.Instance.GetCellGrid();
		float[,] weightMap = Grid.Instance.GetWeightMap();

		Cell enemyCell = Grid.Instance.GetCellFromWorldPoint(animator.transform.position);
		goalCell = Grid.Instance.GetCellFromWorldPoint(CharacterControls.Instance.transform.position);

        var result = await Task.Run(() =>
        {
			path = AStar<Cell>.PathFind(grid, weightMap, enemyCell.GridX, enemyCell.GridY, goalCell.GridX, goalCell.GridY);
            return path;
		});

        if(path.Length <= 1)
        {
            pathIndex = -1;
            return;
        }

        pathIndex = 1;
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find Path
        findPath(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // is path valid? else get new path
        if(goalCell == null || Vector3.Distance(CharacterControls.Instance.transform.position, goalCell.WorldPos) > acceptableGoalError)
        {
            findPath(animator);
        }

        if(pathIndex == -1)
        {
            return;
        }

		// Are we there?
		if (pathIndex >= path.Length - stopRange)
		{
			goalCell = null;
			pathIndex = -1;
            animator.SetBool("InChargingRange", true);
            return;
		}

		// walk along path
		Quaternion desiredRotation = Quaternion.LookRotation(path[pathIndex].WorldPos - animator.transform.position);

        if(Quaternion.Angle(desiredRotation, animator.transform.rotation) < 0.5f)
        {
            animator.transform.rotation = desiredRotation;
        }
        else
        {
			animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
		}

		animator.transform.position = Vector3.MoveTowards(animator.transform.position, path[pathIndex].WorldPos, speed * Time.deltaTime);
        if(Vector3.Distance(animator.transform.position, path[pathIndex].WorldPos) < closeEnough)
        {
            pathIndex++;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Kill old pathfinding task
    }

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
