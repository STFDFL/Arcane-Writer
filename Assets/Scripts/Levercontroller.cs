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
    public bool leverIsNeutral = true;
    public new Animation animation;
    // Use this for initialization
    void Start ()
    {
        
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
        leverIsNeutral = false;
        leverIsPulledLeft = true;
        animation.Play("left");
    }
    public void PullLeverRight()
    {
        leverIsPulledRight = true;
        leverIsNeutral = false;
        leverIsPulledLeft = false;
        animation.Play("right");
    }
    public void PullLeverNeutral()
    {
        leverIsPulledRight = false;
        leverIsNeutral = true;
        leverIsPulledLeft = false;
        animation.Play("Default Take");
    }
}
