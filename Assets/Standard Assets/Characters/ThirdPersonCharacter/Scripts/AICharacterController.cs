using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

namespace UnityStandardAssets.Characters.ThirdPerson

{
	[RequireComponent(typeof (NavMeshAgent))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class AICharacterControl : MonoBehaviour
	{
		public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; } // the character we are controlling
		public Transform target;                                    // target to aim for
		private float distance;
		public float triggerDistance;
		public FirstPersonController FPSC;
		public bool isCombatOver = true;
		public GameObject sceneManager;


		public void Start()
		{
			// get the components on the object we need ( should not be null due to require component so no need to check )
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();

			agent.updateRotation = false;
			agent.updatePosition = true;

			distance = Vector3.Distance(target.position, transform.position);
			Debug.Log(gameObject.name + " is " + distance + "the player");


		}


		public void Update()
		{
            //this is the line we cannot get to work!!!! ////////////////////////////////////////////////////////////////////////

			//TextCombatScript textCombatScript = sceneManager.GetComponent<TextCombatScript>();

			if (target != null)
				agent.SetDestination(target.position);

			if (agent.remainingDistance > agent.stoppingDistance)
				character.Move(agent.desiredVelocity, false, false);
			else
				character.Move(Vector3.zero, false, false);
			distance = Vector3.Distance(target.position, transform.position);

			Debug.Log(gameObject.name + " is " + distance + "the player");

			if( distance < triggerDistance)
			{

				Debug.Log("combat has started");
				gameObject.GetComponent<NavMeshAgent>().enabled = false;
				FPSC.GetComponent<FirstPersonController>().enabled = false;

			}
			if(isCombatOver == true )
			{
				FPSC.GetComponent<FirstPersonController>().enabled = true;
				gameObject.SetActive(false);
			}
		}


		public void SetTarget(Transform target)
		{
			this.target = target;
		}
	}


}
