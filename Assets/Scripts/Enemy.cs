using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{


    public FirstPersonController FPSC;
    private float distance;
    public float triggerDistance;
    public Transform target;
    public bool isThisAlive = true;
    public GameObject sceneManager;
    private TextCombatScript textCombatScript;

    
    
	// Use this for initialization
	void Start ()
    {
        textCombatScript = sceneManager.GetComponent<TextCombatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(target.position, transform.position);

       // Debug.Log(gameObject.name + " is " + distance + "the player");
        if (distance < triggerDistance && isThisAlive == true)
        {
           Debug.Log("combat has started");
            textCombatScript.inCombat = true;

        }
        else if(isThisAlive == false)
        {
            gameObject.SetActive(false);
        }

    }
}
