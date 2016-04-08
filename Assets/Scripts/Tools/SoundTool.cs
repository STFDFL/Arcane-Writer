using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundTool : MonoBehaviour 
{
	public bool randomiseSounds;
	public AudioClip[] sounds;
	public AudioClip soundToBePlayed;
	int soundIndex;
	public float fromSeconds;
	public float toSeconds;
	public float secondsToNextSound;

	// Use this for initialization
	void Start () 
	{
		if (randomiseSounds == true) {
			
			StartCoroutine (Randomiser ());
		} 
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	IEnumerator Randomiser ()
	{
		while (randomiseSounds == true) 
		{
			secondsToNextSound = Random.Range (fromSeconds, toSeconds);
			soundIndex = Random.Range (0, sounds.Length);
			soundToBePlayed = sounds[soundIndex];
			AudioSource.PlayClipAtPoint(soundToBePlayed, transform.position);	
			yield return new WaitForSeconds(secondsToNextSound);
		}
	}
}
