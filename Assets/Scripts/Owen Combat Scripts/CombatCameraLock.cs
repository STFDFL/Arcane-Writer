using UnityEngine;
using System.Collections;

public class CombatCameraLock : MonoBehaviour {

	public GameObject sceneManager;
	private GameObject target;

	void Update () {
		target = sceneManager.GetComponent<TextCombatScript4> ().enemyAnimator;
		gameObject.transform.LookAt (target.transform.position);
	}
}
