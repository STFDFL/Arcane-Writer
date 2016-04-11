using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{
    private float distance;
    private TextCombatScript textCombatScript;
    public FirstPersonController FPSC;
    public GameObject sceneManager;
  
    public bool isThisAlive = true;
    public float triggerDistance;
    public Transform target;
   



    // Use this for initialization
    void Start ()
    {
   
        textCombatScript = sceneManager.GetComponent<TextCombatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        distance = Vector3.Distance(target.position, transform.position);
        if (distance < triggerDistance && isThisAlive == true)
        {
           Debug.Log("combat has started");
            textCombatScript.inCombat = true;
        }
        else if(isThisAlive == false)
        {
            gameObject.SetActive(false);
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
