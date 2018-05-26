using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private EnemyInventory inv;

    //  how far enemy can see
    [SerializeField] private float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    // patrol variables
    public Transform[] patrolPoints;
    private int destPoint = 0;
    //  distance from player
    float playerDistance;

    //  for enemy to find weapons
    Weapon[] availableWeaponsObjects;    

    //  anim declaration
    public Animator GFXAnim;
    private GameObject GFX;

    public LayerMask lookLayer;

    private int arrayPlace;

    public EnemyWeapon weapon;

    void Awake()
    {
        GFX = transform.Find("swat@Idle").gameObject;
        GFXAnim = GFX.GetComponent<Animator>();
    }

    void Start()
    {
        inv = GetComponent<EnemyInventory>();

        //  navmesh
        agent = GetComponent<NavMeshAgent>();
        // disabling auto-breaking allows for continuous movement between points (no slowing down between destination points)
        agent.autoBraking = false;

        anim = GetComponent<Animator>();

        //StartCoroutine(TestFire());
        GoToNextPoint();
    }

    public void SetID(int _arrayPlace)
    {
        arrayPlace = _arrayPlace;
    }

    public int GetID()
    {
        return arrayPlace;
    }

    void Update()
    {

        if (!agent)
        {
            //  navmesh
            agent = GetComponent<NavMeshAgent>();
        }

        //target not in existence till spawned
        if (!target)
        {
            // player's location            
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius, lookLayer);
            if (hitColliders.Length > 0)
            {
                if (hitColliders.Length == 1)
                {
                    target = hitColliders[0].transform;
                }
                else
                {
                    target = hitColliders[Random.Range(0, hitColliders.Length)].transform;
                }
            }

            //  targets distance from player every frame
            playerDistance = Vector3.Distance(target.position, transform.position);
        }


        if (weapon.enemyCrntClip <= 0 && weapon.enemyCrntInventory <= 0)
        {
            Debug.Log("Getting AMMO");
            agent.stoppingDistance = 0;
            agent.SetDestination(GetClosestWeapon().position);
            return;
        }

        //  if player is within enemy's looking radius:
        if (playerDistance <= lookRadius)
        {
            //  sets enemy to chase player 
            //GetComponent<Renderer>().material.color = Color.red;

            //Debug.Log("Chasing player!");
            agent.SetDestination(target.position);
            agent.stoppingDistance = 8;

            if (playerDistance <= agent.stoppingDistance)
            {                
                //  face target
                FaceTarget();

                if (weapon.enemyCrntClip <= 0)
                {
                    // if reload unsuccessful, then no more ammo, go seek ammo
                    if (!weapon.StartReload())
                    {
                        Debug.Log("Enemy has run out of ammo, seeking nearest ammo!");
                        weapon.outOfAmmo = true;
                        agent.stoppingDistance = 0;
                        agent.SetDestination(GetClosestWeapon().position);
                    }
                }
                else
                {
                    //Debug.Log("Enemy firing!");
                    weapon.Fire();
                }
            }
        }

        if (playerDistance > lookRadius)
        {
            GoToNextPoint();
            agent.updateRotation = true;
        }

        //choose next destination in array when agent gets close to current one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
            agent.updateRotation = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.GetComponent<Weapon>() != null){
            Debug.Log("Enemy picked up ammo!");
            inv.enemyPickupWeapon();
            weapon.outOfAmmo = false;
        }
    }

    void GoToNextPoint()
    {
        //  returns if no points set up
        if (patrolPoints.Length == 0)
        {            
            return;
        }

        agent.stoppingDistance = 0;
        //GetComponent<Renderer>().material.color = Color.blue;
        // set agent to go to currently selected destination
        agent.destination = patrolPoints[destPoint].position;
        //Debug.Log("Going to destination");

        //choose next point in array as destination, goes to beginning if at end
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    //  method to face target (player)
    void FaceTarget()
    {
        agent.updateRotation = false;
        //  get direction to player
        Vector3 direction = (target.position - transform.position).normalized;
        // gets rotation pointing to target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //  update own rotation (slerp helps smooth rotation)
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void CheckHasWeapon() // if enemy doesn't have a weapon, seek one out
    {
        if(inv.enemyCurrentWeapon != null)
        {
            return;
        }
        else
        {
            //GetComponent<Renderer>().material.color = Color.green;
            //  set agent to go to nearest weapon        
            agent.stoppingDistance = 0;
            agent.SetDestination(GetClosestWeapon().position);
            Debug.Log("Going to pickup weapon!");
        }
    }    

    Transform GetClosestWeapon()
    {
        availableWeaponsObjects = GameObject.FindObjectsOfType<Weapon>();

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;             
        
        for(int i = 0; i < availableWeaponsObjects.Length; i++)
        {
            float dist = Vector3.Distance(availableWeaponsObjects[i].transform.position, currentPos);
            if(dist < minDist)
            {
                tMin = availableWeaponsObjects[i].transform;
                minDist = dist;
            }
        }
        return tMin;
    }     

    //  purely for editor, sees enemy's look radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    /*bool iKillable.TakeDamage(int amountTaken)
    {
        Debug.Log("Enemy damage taken: " + amountTaken);
        return false;
    }*/
    
}
