using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Khabanh_Patroling_State : StateMachineBehaviour
{
    float timer;

    public float patrolingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    //điểm mốc zombiedidqua
    List<Transform> way_point_List = new List<Transform>();


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //initializeation

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();


        agent.speed = patrolSpeed;
        timer = 0;

        // Move to first waypoint
        GameObject way_point_Cluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in way_point_Cluster.transform)
        {
            way_point_List.Add(t);
        }    

        Vector3 nextPos = way_point_List[Random.Range(0,way_point_List.Count)].position;
        agent.SetDestination(nextPos);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //check sound
        if(SoundManager.Instance.zombieChanels.isPlaying ==false)
        {
            SoundManager.Instance.zombieChanels.clip = SoundManager.Instance.zombieWalking;
            SoundManager.Instance.zombieChanels.PlayDelayed(2f);
        }


        //check if agent arrived at waypoint and move to next waypoint
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(way_point_List[Random.Range(0,way_point_List.Count)].position);
        }

        timer += Time.deltaTime;
        if(timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        //chuyển qua chase state
        float distance_From_player = Vector3.Distance(player.position, animator.transform.position);
        if (distance_From_player < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Stop the agent
        agent.SetDestination(agent.transform.position);
        SoundManager.Instance.zombieChanels.Stop();
    }
}
