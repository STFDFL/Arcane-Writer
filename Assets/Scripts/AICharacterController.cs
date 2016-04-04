using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterController : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        [SerializeField]
        private float distanceToPlayer; // updated distance between enemy and player
        [SerializeField]
        private float distanceToSpawnPoint; // updated distance between enemy and indexed spawn point
        [SerializeField]
        private bool playerIsSpotted = false;
        public float triggerDistance; // between enemy and player, trigger combat
        public float aggroDistance; // between enemy and player, trigger state -chasing-
        public FirstPersonController FPSC;
        public GameObject sceneManager;
        public Transform spawnPoint;
        public Transform patrolTarget;
        private states state;
       
        public float roamingSpeed;
        public Transform[] patrolPoints;
        enum states
        {
            roaming,
            chasing,
            stopChasing
        }
        int index;

        private void Start()
        {
            
            state = states.roaming;
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            agent.updateRotation = false;
            agent.updatePosition = true;
            //randomly pick the first patrol point to go to
            index = UnityEngine.Random.Range(0, patrolPoints.Length);
            patrolTarget = patrolPoints[index];
        }


        private void Update()
        {
            distanceToSpawnPoint = Vector3.Distance(patrolTarget.position, transform.position);

            distanceToPlayer = Vector3.Distance(target.position, transform.position);
           // Debug.Log(gameObject.name + " is " + distance + "the player");
            switch (state)
            {
                case states.roaming:
                    
                    //distanceToSpawnPoint = Vector3.Distance(patrolTarget.position, transform.position);
                    StartCoroutine(ChangePatrolPoint());
                    Debug.Log("im roaming");
                    if ( distanceToPlayer < aggroDistance)
                    {
                        playerIsSpotted = true;
                         
                    }
                    if(playerIsSpotted)
                    {
                        StopCoroutine(ChangePatrolPoint());
                        state = states.chasing;
                    }
                    break;

                case states.chasing:
                    Debug.Log("im chasing");
                    if (target != null)
                        agent.SetDestination(target.position);

                    if (agent.remainingDistance > agent.stoppingDistance)
                        character.Move(agent.desiredVelocity, false, false);
                    else
                        character.Move(Vector3.zero, false, false);
                    if (distanceToPlayer > aggroDistance)
                    {
                        if (target != null)
                        {
                            playerIsSpotted = false;
                            state = states.roaming;
                        }
                            
                    }                   
                    break;

                case states.stopChasing:
                    agent.SetDestination(spawnPoint.position);
                    if(distanceToSpawnPoint <= 1)
                    state = states.roaming;
                    Debug.Log("i stopped chasing");
                    break;
            }     

            if (distanceToPlayer < triggerDistance)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                FPSC.GetComponent<FirstPersonController>().enabled = false; 
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        IEnumerator ChangePatrolPoint()
        {
            while (distanceToSpawnPoint > 5 && playerIsSpotted == false)
            {
                
                gameObject.transform.position = Vector3.Lerp(transform.position, patrolTarget.position, Time.deltaTime * roamingSpeed);
                //agent.SetDestination(patrolTarget.position);
                yield return null;
            }
            yield return new WaitForSeconds(4f);
            index = UnityEngine.Random.Range(0, patrolPoints.Length);
            Debug.Log("" + index);
            patrolTarget = patrolPoints[index];        
            print("Reached the target.");
        }
    }
}