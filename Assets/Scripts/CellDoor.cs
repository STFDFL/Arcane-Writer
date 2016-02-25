using UnityEngine;
using System.Collections;

public class CellDoor : MonoBehaviour {
    public bool pushIsApplied= false;
    public float thrust = 500;
    private Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (pushIsApplied == true)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * thrust);
        }
	}
}
