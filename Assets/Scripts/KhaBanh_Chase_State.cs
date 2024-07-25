using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KhaBanh_Chase_State : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 2f;

    public float stop_Chasing_Distance = 21;
    public float attacking_Distance = 2.5f;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(SoundManager.Instance.zombieChanels.isPlaying == false)
        {
            SoundManager.Instance.zombieChanels.PlayOneShot(SoundManager.Instance.zombieChase);
        }

        agent.SetDestination(player.position);
        animator.transform.LookAt(player.position);

        float distance_from_player = Vector3.Distance(player.position, animator.transform.position);
        if(distance_from_player > stop_Chasing_Distance)
        {
            animator.SetBool("isChasing", false);
        }

        if(distance_from_player < attacking_Distance)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
        SoundManager.Instance.zombieChanels.Stop();
    }
}

