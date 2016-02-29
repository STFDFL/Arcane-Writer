using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
public class SpellBook : MonoBehaviour {



    public GameObject spellBook;
    public bool isTheBookActive = false;
    public FirstPersonController FPSC;
    // Use this for initialization
    void Start ()
    {
        FPSC = GameObject.FindObjectOfType<FirstPersonController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyUp("tab"))
        {
            switch (isTheBookActive)
            {
                case true:
                    spellBook.SetActive(false);
                    isTheBookActive = false;
                    FPSC.GetComponent<FirstPersonController>().enabled = true;
                    break;
                case false:
                    spellBook.SetActive(true);
                    FPSC.GetComponent<FirstPersonController>().enabled = false;
                    isTheBookActive = true;
                    break;
                
            }
           
        }
	}
}
