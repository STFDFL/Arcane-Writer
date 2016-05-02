using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour {

    public bool DoorOpen;
    private bool DoorOpened;
	public bool doorCantBeOpened = false;
	public GameObject sceneManager;
	public InventoryManager inventoryManager;
	public bool doorNeedsKey;
	public GameObject firstPersonCharacter;
	public CursorController cursorController;


	void Start()
	{
		sceneManager = GameObject.Find("SceneManager");
		inventoryManager = sceneManager.GetComponent<InventoryManager>();
		firstPersonCharacter = GameObject.Find ("FirstPersonCharacter");
		cursorController = firstPersonCharacter.GetComponent<CursorController> ();
	}
	// Update is called once per frame
	void Update () {
        

    }

	public void TryToOpen()
	{
		
		if (!DoorOpened &&!doorCantBeOpened )
		{
			if (doorNeedsKey) 
			{
				if (inventoryManager.keysCollected > 0) 
				{
					gameObject.GetComponent<Animation> ().Play ("DoorOpenAnimation");
					gameObject.GetComponent<MeshCollider> ().isTrigger = true;
					DoorOpened = true;
					inventoryManager.keysCollected--;
				}else if(inventoryManager.keysCollected <= 0) 
				{
					cursorController.itsLocked.SetActive (true);
				}
			}else if(!doorNeedsKey) 
			{
				gameObject.GetComponent<Animation> ().Play ("DoorOpenAnimation");
				gameObject.GetComponent<MeshCollider> ().isTrigger = true;
				DoorOpened = true;
			}
		}
	}
}
