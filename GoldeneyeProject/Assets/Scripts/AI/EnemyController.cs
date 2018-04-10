using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    
    //  how far enemy can see
    [SerializeField] private float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    // patrol variables
    public Transform[] patrolPoints;
    private int destPoint = 0;        

	void Start () {
        // player's location
        target = PlayerManager.instance.player.transform;
        //  navmesh
        agent = GetComponent<NavMeshAgent>();
        // disabling auto-breaking allows for continuous movement between points (no slowing down between destination points)
        agent.autoBraking = false;

        GoToNextPoint();
	}	
	
	void Update () {
        //  targets distance from player every frame
        float playerDistance = Vector3.Distance(target.position, transform.position);       
    
        //  if player is within enemy's looking radius:
        if (playerDistance <= lookRadius) {
            //  sets player
            GetComponent<Renderer>().material.color = Color.red;

            agent.SetDestination(target.position);
            agent.stoppingDistance = 8;

            if (playerDistance <= agent.stoppingDistance)
            {
                //  attack target
                //  face target
                FaceTarget();
            }        
                 
        }

        if (playerDistance > lookRadius) {
            GoToNextPoint();    
        }

        //choose next destination in array when agent gets close to current one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint() {
        //  returns if no points set up
        if (patrolPoints.Length == 0)
        {
            return;
        }

        agent.stoppingDistance = 0;
        GetComponent<Renderer>().material.color = Color.blue;
        // set agent to go to currently selected destination
        agent.destination = patrolPoints[destPoint].position;

        //choose next point in array as destination, goes to beginning if at end
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    //  method to face target (player)
    void FaceTarget() {
        //  get direction to player
        Vector3 direction = (target.position - transform.position).normalized;
        // gets rotation pointing to target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //  update own rotation (slerp helps smooth rotation)
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //  purely for editor, sees enemy's look radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
