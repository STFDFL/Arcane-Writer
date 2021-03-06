﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;


 public class CursorController : MonoBehaviour
{

   
    public FirstPersonController FPSC;

    public GameObject playeTorch;
    public GameObject sceneManager;
    //public GameObject tutorialEnemy;
    //raycast
    
    private Ray ray; // the ray that will be shot
    private RaycastHit hit; // variable to hold the object that is hit

    [Header("Target and Cursor__________________________")]
    [Space(3)]
    public Camera playerCamera;
    public InputField inputField;
    public float raycastDistance;
    CursorLockMode wantedMode;
    bool isCursorLocked = false;
    public Image theCursor;
    public GameObject theCursorWhenHitting;

    //the action wrote by the player
    //[HideInInspector]
    public string action;
    //the object hit by the raycast to check against the action chose by the player
    [HideInInspector]
    public string objectHitName;

    [Header("Input Panel / Suggestion Words")]
    [Space(3)]
    public GameObject panel;
    public Text objectName;
    public Text wordsSuggested;


    [Header("Spells")]
    [Space(3)]
    public GameObject pushSymbol;
	public bool iLearnedPush = false;
	public GameObject demonicFireballSymbol;
	public bool iLearnedDemonicFireball = false;
	public GameObject bloodLeechSymbol;
	public bool iLearnedBloodLeech = false;

    [Header("Collected Objects")]
    [Space(3)]
    public GameObject keySymbol;
    public bool iHaveKey = false;

    [Header("Messages Popping Up After Action")]
    [Space(3)]
    public GameObject itsLocked;
    public GameObject itsAlreadyClosed;
    public GameObject iShouldntDoThat;
    public GameObject iLearnedWS;
    public GameObject iLearnedFS;
    public GameObject iLearnedLS;

    [Header("Sounds")]

    [Space(3)]
    public AudioClip inputPopUp;
    public AudioClip pushSpellSound;
    public AudioClip doorBustingSound;
    public AudioClip takeTorchSound;
    public AudioClip takeKeySound;
    public AudioClip openChestSound;
    public AudioClip lootingSound;
    public AudioClip openDoor;
    // Use this for initialization
    void Start()
    {
       
        panel.SetActive(false);
        FPSC = GameObject.FindObjectOfType<FirstPersonController>();
        TextCombatScript textCombatScript = sceneManager.GetComponent<TextCombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
            // The Vector2 class holds the position for a point with only x and y coordinates
            // The center of the screen is calculated by dividing the width and height by half
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            // The method ScreenPointToRay needs to be called from a camera
            // Since we are using the MainCamera of our scene we can have access to it using the Camera.main
            ray = playerCamera.ScreenPointToRay(screenCenterPoint);
           // Debug.DrawRay(playerCamera.transform.position, screenCenterPoint, Color.green);
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
            CheckAnyHitForCursor();
                
                if (Input.GetMouseButtonDown(0))
                {
                    CheckAnyHit();
                    if (hit.transform.tag == "torch")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
					Debug.Log("1");
                        HittingTorch();
                      
                    }
                    else if (hit.transform.tag == "celldoor")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingCelldoor();
                        
                    }
                    else if (hit.transform.tag == "pushspell")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingPushSpell();
                    }
				else if (hit.transform.tag == "firespell")
				{
					AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
					HittingFireSpell();
				}
				else if (hit.transform.tag == "leechspell")
				{
					AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
					HittingLeechSpell();
				}
                    else if (hit.transform.tag == "lever1")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingLever1();
                    }
                    else if (hit.transform.tag == "spikeLever")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingSpikeLever();
                    }
                    else if (hit.transform.tag == "chestKey1")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingChestKey();
                    }
                    else if (hit.transform.tag == "chest1")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingChest();
                    }
                    else if (hit.transform.tag == "chestGas")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingChestGas();
                    }
                    else if (hit.transform.tag == "chestT")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingChestT();
                    }
                    else if (hit.transform.tag == "SquareDoor" || hit.transform.tag == "CurvedDoor")
                    {
                        Debug.Log(3);
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                       HittingSquareDoor();
                    }
                    else if (hit.transform.tag == "lootable")
                    {
                        AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
                        HittingLootable();
                    }
				else if (hit.transform.tag == "Void")
				{
					AudioSource.PlayClipAtPoint(inputPopUp, transform.position);
					HittingVoid();
				}
                else 
                    {
                        inputField.text = "";
                        panel.SetActive(false);
                        objectName.text = "";
                        wordsSuggested.text = "";
                    }
                }   
        }
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isCursorLocked == false)
            {
                //lock
                Cursor.lockState = wantedMode = CursorLockMode.Locked;
                SetCursorState();
                isCursorLocked = true;
            }
            else if(isCursorLocked == true)
            {
                //release
                Cursor.lockState = wantedMode = CursorLockMode.None;
                SetCursorState();
                isCursorLocked = false;
            }
        }
       
    }
    // the following methods decide what to display in the input panel infos
    public void HittingTorch()
    {
        objectName.text = "A Torch";
        wordsSuggested.text = "take\n" +"blow\n" +"touch";
        objectHitName = "torch";
    }

    public void HittingCelldoor()
    {
       
        objectName.text = "The Celldoor";

        if(iLearnedPush == false)
        wordsSuggested.text = "open\n" +"close\n" +"";
        else if (iLearnedPush == true)
            wordsSuggested.text = "open\n" + "close\n" + "witch scream";

        objectHitName = "celldoor";
    }
    public void HittingPushSpell()
    {
      
        objectName.text = "WITCH SCREAM";
        wordsSuggested.text = "read\n";
        objectHitName = "pushspell";

    }
	public void HittingFireSpell()
	{

		objectName.text = "DEMONIC FIRE";
		wordsSuggested.text = "read\n";
		objectHitName = "firespell";

	}
	public void HittingLeechSpell()
	{

		objectName.text = "BLOOD LEECH";
		wordsSuggested.text = "read\n";
		objectHitName = "leechspell";

	}
    public void HittingLever1()
    {
       
        objectName.text = "Lever";
        wordsSuggested.text = "pull left\n" + "pull right\n";
        objectHitName = "lever1";

    }
    public void HittingSpikeLever()
    {

        objectName.text = "Spike Trap";
        wordsSuggested.text = "pull left\n" + "pull right\n";
        objectHitName = "spikeLever";

    }
    public void HittingChestKey()
    {

        objectName.text = "Key";
        wordsSuggested.text = "take";
        objectHitName = "chestKey1";

    }
    public void HittingChest()
    {

        objectName.text = "Chest";
        wordsSuggested.text = "open";
        objectHitName = "chest1";

    }
    public void HittingChestGas()
    {

        objectName.text = "Chest";
        wordsSuggested.text = "open";
        objectHitName = "chestGas";

    }
    public void HittingChestT()
    {

        objectName.text = "Chest";
        wordsSuggested.text = "open";
        objectHitName = "chestT";

    }
    public void HittingLootable()
    {

        objectName.text = "Crate";
        wordsSuggested.text = "loot";
        objectHitName = "lootable";

    }
    public void HittingSquareDoor()
    {
        Debug.Log(2);
        objectName.text = "Door";
        wordsSuggested.text = "open";
        objectHitName = "SquareDoor";

    }
	public void HittingVoid()
	{
		Debug.Log(2);
		objectName.text = "Door";
		wordsSuggested.text = "open";
		objectHitName = "Void";

	}
    // Method to lock and center cursor 
    void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }
    public void HidePanel()
    {
        action = inputField.text;
        panel.SetActive(false);
        objectName.text = "";
        wordsSuggested.text = "";
        inputField.text = "";
        inputField.DeactivateInputField();
        FPSC.GetComponent<FirstPersonController>().enabled = true;
    }
    // this method checks the words inserted by the player in the input 
    public void CheckForWords()
    {
        if(objectHitName == "firespell" && action == "read")
        {
            demonicFireballSymbol.SetActive(true);
            iLearnedDemonicFireball = true;
            GameObject fireS;
            fireS = GameObject.FindGameObjectWithTag("firespell");
            fireS.SetActive(false);
            iLearnedWS.SetActive(true);
        }
		if(objectHitName == "leechspell" && action == "read")
		{
			bloodLeechSymbol.SetActive(true);
			iLearnedBloodLeech = true;
			GameObject leechS;
			leechS = GameObject.FindGameObjectWithTag("leechspell");
			leechS.SetActive(false);
            iLearnedWS.SetActive(true);
        }
		if(objectHitName == "pushspell" && action == "read")
		{
			pushSymbol.SetActive(true);
			iLearnedPush = true;
			//hit.transform.gameObject.SetActive(false);
			GameObject pushS;
			pushS = GameObject.FindGameObjectWithTag("pushspell");
			pushS.SetActive(false);
            iLearnedWS.SetActive(true);
        }
        if (objectHitName == "celldoor")
        {
            GameObject tutorialDoor = GameObject.Find("Tutorial Door");
            CellDoor cellDoorScript = tutorialDoor.GetComponent<CellDoor>();
            if (action == "open")
            {
                itsLocked.SetActive(true);
            }
            else if (action == "close")
            {
                itsAlreadyClosed.SetActive(true);
            }
            else if (action == "witch scream" && iLearnedPush == true)
            {
                StartCoroutine(BustDoor());
                AudioSource.PlayClipAtPoint(pushSpellSound, transform.position);  
            }
			else
			{
				iShouldntDoThat.SetActive(true);
			}
            
        }
        if (objectHitName == "SquareDoor")
        {
            Debug.Log(1);
            DoorOpenScript dooropenScript = hit.transform.GetComponent<DoorOpenScript>();
			if (dooropenScript.doorCantBeOpened )
			{
				itsLocked.SetActive(true);
			}
            else if (action == "open" )
            {
                AudioSource.PlayClipAtPoint(openDoor, transform.position);
				dooropenScript.TryToOpen ();
			}else
			{
				iShouldntDoThat.SetActive(true);
			}

            
        }
        if (objectHitName == "lever1")
        {
            GameObject lever1 = GameObject.Find("lever1");
            Levercontroller leverController = lever1.GetComponent<Levercontroller>();
            if (action == "pull left")
            {
                leverController.PullLeverLeft();
            }
            else if (action == "pull right")
            {
                leverController.PullLeverRight();
            }

           
        }
        if (objectHitName == "spikeLever")
        {
            GameObject spikeLever = GameObject.Find("spikeLever");
            SpikeLever spikeLeverController = spikeLever.GetComponent<SpikeLever>();
            if (action == "pull left")
            {
                spikeLeverController.PullLeverLeft();
            }
            else if (action == "pull right")
            {
                spikeLeverController.PullLeverRight();
            }
			else
			{
				iShouldntDoThat.SetActive(true);
			}

        }
        if (objectHitName == "chestKey1")
        {
            if (action == "take")
            {
                keySymbol.SetActive(true);
                iHaveKey = true;
                //hit.transform.gameObject.SetActive(false);
                GameObject cKey;
                cKey = GameObject.FindGameObjectWithTag("chestKey1");
                cKey.SetActive(false);
                AudioSource.PlayClipAtPoint(takeKeySound, transform.position);
				GameObject sceneManager = GameObject.Find("SceneManager");
				InventoryManager inventoryManager = sceneManager.GetComponent<InventoryManager>();
				inventoryManager.KeyFound ();
            }  
			else
			{
				iShouldntDoThat.SetActive(true);
			}
        }
        if (objectHitName == "chest1")
        {
            GameObject chest = GameObject.Find("chest1");
            if(iHaveKey)
            {
                if (action == "open")
                {
                    keySymbol.SetActive(false);
                    iHaveKey = false;
                    Debug.Log("the chest is now open");
                    chest.GetComponent<Animation>().Play("ChestAnim");
                    AudioSource.PlayClipAtPoint(openChestSound, transform.position);
                    //// to activate the alarm trap
                    GameObject alarmTrap = GameObject.Find("Alarm Trap");
                    AlarmTrap alarmTrapScript = alarmTrap.GetComponent<AlarmTrap>();
                    alarmTrapScript.actvateTrap = true;
                }
				else
				{
					iShouldntDoThat.SetActive(true);
				}
            }
        }
        if (objectHitName == "chestGas")
        {
            GameObject chestGas = GameObject.Find("chestGas");
            GameObject chestGasTrap = GameObject.Find("gasT");
            GasTrap gasTrap = chestGasTrap.GetComponent<GasTrap>();
            if (action == "open")
                {
                
                chestGas.GetComponent<Animation>().Play("ChestAnim");
                    AudioSource.PlayClipAtPoint(openChestSound, transform.position);
                gasTrap.trapEnabled = true;
                }
			else
			{
				iShouldntDoThat.SetActive(true);
			}
        }
        if (objectHitName == "chestT")
        {
            GameObject chestT = GameObject.Find("chestT");
                if (action == "open")
                {
                    chestT.GetComponent<Animation>().Play("ChestAnim");
                    AudioSource.PlayClipAtPoint(openChestSound, transform.position);
                }
			else
			{
				iShouldntDoThat.SetActive(true);
			}
        }
        if (objectHitName == "torch")
        {
            
            if (action == "take")
            {
				if (hit.transform.gameObject.tag == "torch")
                hit.transform.gameObject.SetActive(false);
				
                playeTorch.SetActive(true);
                Debug.Log("torch taken");
                AudioSource.PlayClipAtPoint(takeTorchSound, transform.position);
            }
			else
			{
				iShouldntDoThat.SetActive(true);
			}
        }
        if (objectHitName == "lootable")
        {

            if (action == "loot")
            {
                Debug.Log("looting");
                //AudioSource.PlayClipAtPoint(lootingSound, transform.position);
                LootThis lootableObj = hit.transform.GetComponent<LootThis>();
                lootableObj.Loot();
            }
			else
			{
				iShouldntDoThat.SetActive(true);
			}
		}
		if (objectHitName == "Void")
		{

			if (action == "open")
			{
				iShouldntDoThat.SetActive(true);
			}
			else
			{
				iShouldntDoThat.SetActive(true);
			}
		}
        
    }

    public void CheckAnyHit()
    {
        //Debug.Log(hit.transform.tag);
        if (hit.transform.tag == "torch" ||
            hit.transform.tag == "celldoor" ||
            hit.transform.tag == "lever1" ||
            hit.transform.tag == "spikeLever" ||
            hit.transform.tag == "chestKey1" ||
            hit.transform.tag == "chest1" ||
            hit.transform.tag == "chestGas" ||
            hit.transform.tag == "chestT" ||
            hit.transform.tag == "lootable" ||
            hit.transform.tag == "SquareDoor" ||
            hit.transform.tag == "CurvedDoor" ||
			hit.transform.tag == "Void" ||
			hit.transform.tag == "leechspell" ||
			hit.transform.tag == "firespell" ||
            hit.transform.tag == "pushspell")
        {
            
            panel.SetActive(true);
            inputField.ActivateInputField();
            FPSC.GetComponent<FirstPersonController>().enabled = false;
           
        }
    }
    // cursor shape function
    public void CheckAnyHitForCursor()
    {
        Debug.Log(hit.transform.tag);
        if (hit.transform.tag == "torch" ||
            hit.transform.tag == "celldoor" ||
            hit.transform.tag == "lever1" ||
            hit.transform.tag == "spikeLever" ||
            hit.transform.tag == "chestKey1" ||
            hit.transform.tag == "SquareDoor" ||
            hit.transform.tag == "CurvedDoor"||
            hit.transform.tag == "chest1" ||
            hit.transform.tag == "chestGas" ||
            hit.transform.tag == "chestT" ||
            hit.transform.tag == "lootable" ||
			hit.transform.tag == "Void" ||
			hit.transform.tag == "leechspell" ||
			hit.transform.tag == "firespell" ||
            hit.transform.tag == "pushspell")
        {
            theCursorWhenHitting.SetActive(true);
        }
        else
            theCursorWhenHitting.SetActive(false);
    }
    IEnumerator BustDoor()
    {
        // GameObject tutorialDoor = GameObject.Find("Tutorial Door");
        GameObject tutorialDoor = hit.transform.gameObject;
        CellDoor cellDoorScript = tutorialDoor.GetComponent<CellDoor>();
        yield return new WaitForSeconds(2);
        cellDoorScript.pushIsApplied = true;
        AudioSource.PlayClipAtPoint(doorBustingSound, transform.position);
    }
    //IEnumerator MonsterAppear()
    //{
    //    tutorialEnemy.SetActive(true);
    //    yield return new WaitForSeconds(3);
    //    AudioSource.PlayClipAtPoint(monsterSound1, transform.position);
    //}
}
