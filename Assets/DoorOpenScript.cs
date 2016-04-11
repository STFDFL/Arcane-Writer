using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {

    public bool DoorOpen;
    private bool DoorOpened;

	// Update is called once per frame
	void Update () {
        
        if (DoorOpen == true && !DoorOpened){
            gameObject.GetComponent<Animation>().Play("DoorOpenAnimation");
            DoorOpened = true;
        }
    }
}
