using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
        public float triggerDistance;
        public FirstPersonController FPSC;
        public GameObject sceneManager;
        

        private void Start()
        {
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
            if (distance > triggerDistance)
            {
                if (target != null)
                    agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);
                distance = Vector3.Distance(target.position, transform.position);

               // Debug.Log(gameObject.name + " is " + distance + "the player");
            }
               

            else if (distance < triggerDistance)
            {
               
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                FPSC.GetComponent<FirstPersonController>().enabled = false;
                
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}