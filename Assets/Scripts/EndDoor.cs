using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {

    GameObject triggerObject;
    GasTrap gasTrap;
    public GameObject openDoor;
    public GameObject closedDoor;
    // Use this for initialization
    void Start ()
    {
        triggerObject = GameObject.Find("Gas Trap");
        gasTrap = triggerObject.GetComponent<GasTrap>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	if(gasTrap.trapEnabled == true)
        {
            closedDoor.SetActive(false);
            openDoor.SetActive(true);

        }
	}
}
