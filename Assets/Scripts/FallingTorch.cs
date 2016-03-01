using UnityEngine;
using System.Collections;

public class FallingTorch : MonoBehaviour {

	private Rigidbody rb;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter()
	{
		rb.GetComponent<Rigidbody>().useGravity = true;
	}

}
