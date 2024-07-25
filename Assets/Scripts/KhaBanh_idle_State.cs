using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhaBanh_idle_State : StateMachineBehaviour
{
    //ctrl + k + u để mở comment

    float timer;
    public float idleTime = 0f;

    Transform player;

    public float dectectionAreaRadius = 10f;

    //enter idle state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //transition to patrol state
        timer += Time.deltaTime;

        if (timer > idleTime)
        {
            animator.SetBool("isPatroling", true);
        }

        //transition to chase state
        float distance_From_player = Vector3.Distance(player.position, animator.transform.position);
        if (distance_From_player < dectectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

}
