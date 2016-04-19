using UnityEngine;
using System.Collections;

public class LoadTutorial : MonoBehaviour {
	public int levelToLoad;

	// Use this for initialization
	void Start () {
		Application.LoadLevel (levelToLoad);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
