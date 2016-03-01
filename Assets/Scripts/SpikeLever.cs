using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class SpikeLever : MonoBehaviour
{
   
    public bool leverIsPulledLeft = false;
    public bool leverIsPulledRight = false;
    public GameObject trap;
    public AudioClip leverSound;
    public Animation animationLever;
    // Use this for initialization
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PullLeverLeft()
    {
        SpikeTrap spikeTrap = trap.GetComponent<SpikeTrap>();
        if (leverIsPulledLeft == false)
        {
            animationLever.Play("left");
            AudioSource.PlayClipAtPoint(leverSound, transform.position);
            leverIsPulledRight = false;
            leverIsPulledLeft = true;
            spikeTrap.trapEnabled = false;
        }


    }
    public void PullLeverRight()
    {
        SpikeTrap spikeTrap = trap.GetComponent<SpikeTrap>();
        if (leverIsPulledRight == false)
        {
            animationLever.Play("right");
            AudioSource.PlayClipAtPoint(leverSound, transform.position);
            leverIsPulledRight = true;
            leverIsPulledLeft = false;
            spikeTrap.trapEnabled = true;
        }

    }
}
