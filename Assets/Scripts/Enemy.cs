using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{
    private float distance;
    private TextCombatScript4 textCombatScript;
    public FirstPersonController FPSC;
    public GameObject sceneManager;
  
    public bool isThisAlive = true;
    public float triggerDistance;
    public Transform target;
    public bool combatOn;
   



    // Use this for initialization
    void Start ()
    {
   
        textCombatScript = sceneManager.GetComponent<TextCombatScript4>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        distance = Vector3.Distance(target.position, transform.position);
        if (distance < triggerDistance && isThisAlive == true)
        {
           Debug.Log("combat has started");
            //textCombatScript.inCombat = true;
			textCombatScript.inCombat = true;
            //combatOn = true;
        }
        else if(isThisAlive == false)
        {
            gameObject.SetActive(false);
            combatOn = false;
			textCombatScript.inCombat = false;
        }
		if (textCombatScript.AIHealth <= 0) {
			gameObject.SetActive (false);
			textCombatScript.playerAttackNumber = 0;
		}
    }
    public void AIState()
    {

        
    }
}
