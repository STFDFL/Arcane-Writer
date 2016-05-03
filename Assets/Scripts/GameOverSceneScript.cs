using UnityEngine;
using System.Collections;

public class GameOverSceneScript : MonoBehaviour {
	public string levelToLoad;
	public int zero = 0;
	// Use this for initialization
	void Start () {
	
		PlayerPrefs.SetInt("potionsInv", zero);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Space))
			Application.LoadLevel (levelToLoad);
	}
}
