using UnityEngine;
using System.Collections;

public class CombatCameraLock : MonoBehaviour {

	public GameObject sceneManager;
	private Enemy target;

	void Update () {
		target = sceneManager.GetComponent<TextCombatScript4> ().enemy;
		gameObject.transform.LookAt (target.transform.position);
	}
}
