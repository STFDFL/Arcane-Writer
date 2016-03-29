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
        private float distance;
        private float distanceToSpawnPoint;
        public float triggerDistance;
        public float aggroDistance;
        public FirstPersonController FPSC;
        public GameObject sceneManager;
        public Transform spawnPoint;
        public Transform patrolTarget;
        private states state;
        float smoothing = 0.01f;
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

            distance = Vector3.Distance(target.position, transform.position);
            //Debug.Log(gameObject.name + " is " + distance + "the player");
        }


        private void Update()
        {
            
            distanceToSpawnPoint = Vector3.Distance(spawnPoint.position, transform.position);
            distance = Vector3.Distance(target.position, transform.position);
            Debug.Log(gameObject.name + " is " + distance + "the player");
            switch (state)
            {
                case states.roaming:
                    Debug.Log("im roaming");
                    
                    agent.SetDestination(spawnPoint.position);
                    
                    patrolTarget = patrolPoints[index];
                    StartCoroutine(ChangePatrolPoint());
                    if (distance > triggerDistance && distance < aggroDistance)
                    {
                        if (target != null)
                            state = states.chasing;
                        StopCoroutine(ChangePatrolPoint());
                        
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
                    if (distance > aggroDistance)
                    {
                        if (target != null)
                            state = states.stopChasing;
                    }
                    
                    break;

                case states.stopChasing:
                    agent.SetDestination(spawnPoint.position);
                    if(distanceToSpawnPoint <= 1)
                    state = states.roaming;
                    Debug.Log("i stopped chasing");
                    break;
            }
             
           
               

            if (distance < triggerDistance)
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
            
            
            while (Vector3.Distance(transform.position, patrolTarget.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, patrolTarget.position, smoothing * Time.deltaTime);

                yield return null;
            }
            
            
            //agent.SetDestination(patrolTarget.position);
            

            print("Reached the target.");

            
        }
    }
}