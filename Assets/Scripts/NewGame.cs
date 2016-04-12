using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	public string nextLevel;

	public void LoadLevel()
	{
		Application.LoadLevel(nextLevel);
	}
}
