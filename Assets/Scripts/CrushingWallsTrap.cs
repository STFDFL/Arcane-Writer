﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CrushingWallsTrap : MonoBehaviour {
    public bool playerActivateTrap = false;
    public float speed;
    public GameObject pressurePlate;
    public GameObject leftWalls;
    public GameObject rightWalls;
    public bool wallsAreMoving = false;
    public Transform leftWallsT;
    public Transform rightWallsT;
	public FirstPersonController FPSC;
	public AudioSource movingWallsSound;
	public AudioClip stopWallsSound;
	public AudioClip pPSound;
	public float timeLeft = 5f;
	private float distL;
	private float distR;
	private bool wS = false;
    // Use this for initialization
    void Start ()
    {
		movingWallsSound = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		distL = Vector3.Distance(leftWallsT.position, pressurePlate.transform.position);
		distR = Vector3.Distance(rightWallsT.position, pressurePlate.transform.position);
        if (wallsAreMoving == true)
        {
            leftWalls.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            rightWalls.transform.Translate(Vector3.back * Time.deltaTime * speed);
            if (leftWallsT && rightWallsT)
            {
				if (wallsAreMoving == true && wS == true) {
					StartCoroutine (WallSoundC ());
				}


                //Debug.Log("distL:"+distL);
                //Debug.Log("distR:"+distR);
                if (distL >= 5.1f && distR >= 4.6f)
                {
					//FPSC.GetComponent<FirstPersonController>().enabled = false;
                    wallsAreMoving = false;
					AudioSource.PlayClipAtPoint(stopWallsSound, transform.position);
                }
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			AudioSource.PlayClipAtPoint(pPSound, transform.position);
           // Debug.Log("player enter!");
            pressurePlate.transform.Translate(Vector3.down * Time.deltaTime * speed);
            playerActivateTrap = true;
            wallsAreMoving = true;
			wS = true;
        }
    }
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if(distL >= 5.1f && distR >= 4.6f)
			{
				FPSC.GetComponent<FirstPersonController>().enabled = false;
				timeLeft -= Time.deltaTime;
				if (timeLeft < 0)
				{
					Application.LoadLevel("Game Over 1" );
				}
			}
		}
	}
	IEnumerator WallSoundC()
	{
		wS = false;
		movingWallsSound.Play ();
		yield return new WaitForSeconds (5.5f);
	}
}
