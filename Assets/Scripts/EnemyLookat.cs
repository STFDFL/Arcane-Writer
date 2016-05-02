using UnityEngine;
using System.Collections;

public class EnemyLookat : MonoBehaviour {
		public Transform target;

		void Update() {
		// Rotate the camera every frame so it keeps looking at the target 
		transform.LookAt(target);
		transform.rotation *= Quaternion.Euler (0, 180, 0);
		}
	}