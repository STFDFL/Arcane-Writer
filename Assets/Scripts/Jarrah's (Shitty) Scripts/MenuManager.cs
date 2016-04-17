using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject player;
	public Canvas menuItems;
	public AudioSource wakeSound;

	private Animation playerAnim;
	public float waitTime;

	void Start()
	{
		playerAnim = player.GetComponent<Animation>();

		Cursor.visible = true; 
		menuItems.enabled = true;
	}

	public void LoadTutorial()
	{
		wakeSound.Play ();
		menuItems.enabled = false;
		playerAnim.enabled = true;
		StartCoroutine (WaitForAnim ());
	}


	public void MainMenu()
	{
		Application.LoadLevel (0);
	}


	public void Exit()
	{
		Application.Quit ();
	}



	IEnumerator WaitForAnim ()
	{
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel(1);
	}
}
