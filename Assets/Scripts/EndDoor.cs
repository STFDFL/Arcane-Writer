using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {

    GameObject triggerObject;
    AlarmTrap alarmTrap;
    public GameObject openDoor;
    public GameObject closedDoor;
    // Use this for initialization
    void Start ()
    {
        triggerObject = GameObject.Find("Alarm Trap");
        alarmTrap = triggerObject.GetComponent<AlarmTrap>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	if(alarmTrap.actvateTrap == true)
        {
            closedDoor.SetActive(false);
            openDoor.SetActive(true);

        }
	}
}
