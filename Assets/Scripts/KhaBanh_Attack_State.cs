using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class KhaBanh_Attack_State : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stop_Attacking_Distance = 2.5f;
    public float rotationSpeed = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(SoundManager.Instance.zombieChanels.isPlaying == false)
        {
            SoundManager.Instance.zombieChanels.PlayOneShot(SoundManager.Instance.zombieAttack);
        }

        // Mày nhìn cái chó gì
        LookAtPlayer();

        float distance_from_Player = Vector3.Distance(agent.transform.position, player.position);

        if (distance_from_Player > stop_Attacking_Distance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.zombieChanels.Stop();
    }
}