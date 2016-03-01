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
   // public Animation animationGate;
    public GameObject gate;
    public bool gateIsOpen = false;
    // Use this for initialization
    void Start ()
    {
        //gate.GetComponent<Gate>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //animation.Play("right");
       // animation["On"].wrapMode = WrapMode.Once;
       
      
    }

    public void PullLeverLeft()
    {
        leverIsPulledRight = false;
        leverIsPulledLeft = true;
        animationLever.Play("left");
        gateIsOpen = true;
        if (gateIsOpen == true)
        {
            Debug.Log("im opening");
            gate.GetComponent<Animation>().Play("openGate");
        }
    }
    public void PullLeverRight()
    {
        leverIsPulledRight = true;

        leverIsPulledLeft = false;
        animationLever.Play("right");
        gateIsOpen = false;
        if (gateIsOpen == false)
        {
            gate.GetComponent<Animation>().Play("closeGate");
            
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
