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
    //public AudioClip trapSound;
    //public AudioClip screamSound;
    public bool trapEnabled = false;
    private bool trapHasKilled = false;
    public bool playerIsIn = false;
    public float gasTimeLeft;
    private float timeLeft = 5f;
    //public Animation trapAnimation;
    // Use this for initialization
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
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player is out!");
            playerIsIn = false;
        }
    }
    public void ActivateGasTrap()
    {
        ParticleSystem gasParticle =  gameObject.GetComponent<ParticleSystem>();
        gasParticle.Emit(1);
        //gasParticle.GetComponent<ParticleEmitter>().enabled = true;
        //gameObject.GetComponent<Animation>().Play("SpikeTrap");
        //AudioSource.PlayClipAtPoint(screamSound, transform.position);
        //AudioSource.PlayClipAtPoint(trapSound, transform.position);
        //trapHasKilled = true;
    }
}
