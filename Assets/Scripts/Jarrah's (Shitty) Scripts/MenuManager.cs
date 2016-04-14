using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	void Start()
	{
		Cursor.visible = true; 
	}

	public void LoadTutorial()
	{
		Application.LoadLevel(1);
	}


	public void MainMenu()
	{
		Application.LoadLevel (0);
	}


	public void Exit()
	{
		Application.Quit ();
	}
}
