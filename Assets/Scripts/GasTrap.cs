using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class GasTrap : MonoBehaviour
{
    //public FirstPersonController FPSC;
 
    public bool trapEnabled = false;
    private bool trapHasKilled = false;
    public bool playerIsIn = false;
    public float gasTimeLeft;
    private float timeLeft = 5f;
    public GameObject sceneManager;
    public float gasDamage;
	TextCombatScript4 textCombatScript;
	public AudioClip coughSound;
	public bool isCoughing = false;

    void Start()
    {
		textCombatScript = sceneManager.GetComponent<TextCombatScript4>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player enter!");
            playerIsIn = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (trapEnabled == true && playerIsIn == true)
            {
                Debug.Log("trap activated");
                ActivateGasTrap();
                StartCoroutine(GasDamageOverTime());
				StartCoroutine(Cough());
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player is out!");
            playerIsIn = false;
            StopCoroutine(GasDamageOverTime());
        }
    }
    public void ActivateGasTrap()
    {
        ParticleSystem gasParticle =  gameObject.GetComponent<ParticleSystem>();
        gasParticle.Emit(1);
    }
    IEnumerator GasDamageOverTime()
    {
        //TextCombatScript4 textCombatScript = sceneManager.GetComponent<TextCombatScript4>();
        yield return new WaitForSeconds(3);
        textCombatScript.playerHealth = textCombatScript.playerHealth - (gasDamage * Time.deltaTime); 

        //		Debug.Log ("This Happens");
    }
	IEnumerator Cough()
	{
		if (!isCoughing) {
			isCoughing = true;
			AudioSource.PlayClipAtPoint (coughSound, transform.position);
			yield return new WaitForSeconds(8);
			isCoughing = false;
		}




	}
}
