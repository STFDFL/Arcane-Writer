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
    public GameObject gasParticle;
    public AudioClip trapSound;
    public AudioClip screamSound;
    public bool trapEnabled = true;
    private bool trapHasKilled = false;
    public float gasTimeLeft;
    private float timeLeft = 5f;
    public Animation trapAnimation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(trapEnabled == true)
        {
            ActivateGasTrap();
        }
        if (trapHasKilled == true)
        {
            FPSC.GetComponent<FirstPersonController>().enabled = false;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                SceneManager.LoadScene("Level 1");
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(trapEnabled == true)
        {
            if (other.attachedRigidbody)
            {

            }
        }
    }
    public void ActivateGasTrap()
    {
        gasParticle.SetActive(true);
        //gameObject.GetComponent<Animation>().Play("SpikeTrap");
        AudioSource.PlayClipAtPoint(screamSound, transform.position);
        AudioSource.PlayClipAtPoint(trapSound, transform.position);
        //trapHasKilled = true;
    }
}
