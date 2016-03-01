using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class Levercontroller : MonoBehaviour {

    public bool leverIsPulledLeft = false;
    public bool leverIsPulledRight = false;
    public Animation animationLever;
    public GameObject gate;
    public bool gateIsOpen = false;
    public AudioClip leverSound;
    public AudioClip gateTransition;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }

    public void PullLeverLeft()
    {
        if(leverIsPulledLeft == false)
        {
            AudioSource.PlayClipAtPoint(leverSound, transform.position);
            leverIsPulledRight = false;
            leverIsPulledLeft = true;
            animationLever.Play("left");
            gateIsOpen = true;
            if (gateIsOpen == true)
            {
                AudioSource.PlayClipAtPoint(gateTransition, gate.transform.position);
                gate.GetComponent<Animation>().Play("openGate");
            }
        }

        
    }
    public void PullLeverRight()
    {
        if (leverIsPulledRight == false)
        {
            AudioSource.PlayClipAtPoint(leverSound, transform.position);
            leverIsPulledRight = true;
            leverIsPulledLeft = false;
            animationLever.Play("right");
            gateIsOpen = false;
            if (gateIsOpen == false)
            {
                AudioSource.PlayClipAtPoint(gateTransition, gate.transform.position);
                gate.GetComponent<Animation>().Play("closeGate");
            }
        }
         
    }
    //public void PullLeverNeutral()
    //{
    //    leverIsPulledRight = false;
    //    leverIsNeutral = true;
    //    leverIsPulledLeft = false;
    //    //animationLever.Play("Default Take");
        
       
    //}
}
