using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class GasTrap : MonoBehaviour
{
    public FirstPersonController FPSC;
 
    public bool trapEnabled = false;
    private bool trapHasKilled = false;
    public bool playerIsIn = false;
    public float gasTimeLeft;
    private float timeLeft = 5f;
    public GameObject sceneManager;
    public float gasDamage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        //if (trapHasKilled == true)
        //{
        //    FPSC.GetComponent<FirstPersonController>().enabled = false;
        //    timeLeft -= Time.deltaTime;
        //    if (timeLeft < 0)
        //    {
        //        SceneManager.LoadScene("Level 1");
        //    }
        //}
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
        TextCombatScript textCombatScript = sceneManager.GetComponent<TextCombatScript>();
        yield return new WaitForSeconds(3);
        textCombatScript.playerHealth = textCombatScript.playerHealth - (gasDamage * Time.deltaTime); 

        //		Debug.Log ("This Happens");
    }
}
