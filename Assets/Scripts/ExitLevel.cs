using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitLevel : MonoBehaviour {


    public string nextLevel;
	// Use this for initialization
	void Start ()
    {

	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collider other)
    {

            if (other.gameObject.tag == "Player")
            {
            //SceneManager.LoadScene(nextLevel);
            Application.LoadLevel(nextLevel);
                Debug.Log("going to next scene!");
               
            }
        
    }
}
