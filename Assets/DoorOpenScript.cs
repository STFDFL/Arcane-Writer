using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {

    public bool DoorOpen;
    private bool DoorOpened;
	public bool doorCantBeOpened = false;

	// Update is called once per frame
	void Update () {
        
		if (DoorOpen == true && !DoorOpened &&!doorCantBeOpened)
		{
            gameObject.GetComponent<Animation>().Play("DoorOpenAnimation");
            DoorOpened = true;
        }
    }
}
