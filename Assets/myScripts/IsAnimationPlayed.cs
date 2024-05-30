using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class IsAnimationPlayed : StateMachineBehaviour
{
    

    private GameObject[] poses;

    private string[] names = { "lay45", "sit90", "left", "lay180" };

    private string nextPose;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < names.Length; i++){
            if (animator.GetNextAnimatorStateInfo(layerIndex).IsName(names[i]))
            {
                nextPose = names[i];
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        poses = animator.gameObject.GetComponent<myPoseList>().poseList;
        animator.Update(0);
        
            for (int i = 0; i < poses.Length; i++)
            {   
                
                poses[i].SetActive(false);
                
            }

            for (int i = 0; i < poses.Length; i++)
            {
                if(nextPose == names[i]){
                    poses[i].SetActive(true);
                }
            }
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
