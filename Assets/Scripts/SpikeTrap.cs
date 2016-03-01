using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SpikeTrap : MonoBehaviour {

    public AudioClip trapSound;
    public AudioClip screamSound;
    public bool trapEnabled = true;
    public bool trapHasKilled = false;
    public float timeLeft = 5f;
    public Animation trapAnimation;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(trapHasKilled == true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Application.LoadLevel("Tutorial");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(trapEnabled == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("trap activated!");
                ActivateSpikeTrap();
            }
        }
    }
    public void ActivateSpikeTrap()
    {
        //trapAnimation.Play("SpikeTrap");
       gameObject.GetComponent<Animation>().Play("SpikeTrap");
        AudioSource.PlayClipAtPoint(screamSound, transform.position);
        AudioSource.PlayClipAtPoint(trapSound, transform.position);
        trapHasKilled = true;
    }
}
