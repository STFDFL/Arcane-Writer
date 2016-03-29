using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;


 public class CursorController : MonoBehaviour
{

    [HideInInspector]
    public FirstPersonController FPSC;

    public GameObject playeTorch;
    public GameObject sceneManager;
    public GameObject tutorialEnemy;
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
    [HideInInspector]
    public string action;
    //the object hit by the raycast to check against the action chose by the player
    [HideInInspector]
    public string objectHitName;

    [Header("Input Panel / Suggestion Words __________________________")]
    [Space(3)]
    public GameObject panel;
    public Text objectName;
    public Text wordsSuggested;


    [Header("Spells____________________________________________________")]
    [Space(3)]
    public GameObject pushSymbol;
    private bool iLearnedPush = false;

    [Header("Collected Objects__________________________")]
    [Space(3)]
    public GameObject keySymbol;
    public bool iHaveKey = false;

    [Header("Messages Popping Up After Action__________________________")]
    [Space(3)]
    public GameObject itsLocked;
    public GameObject itsAlreadyClosed;

    [Header("Sounds____________________________________________________")]

    [Space(3)]
    public AudioClip inputPopUp;
    public AudioClip pushSpellSound;
    public AudioClip doorBustingSound;
    public AudioClip takeTorchSound;
    public AudioClip takeKeySound;
    public AudioClip openChestSound;
    // Use this for initialization
    void Start()
    {
       
        //Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        FPSC = GameObject.FindObjectOfType<FirstPersonController>();
        TextCombatScript textCombatScript = sceneManager.GetComponent<TextCombatScript>();

    }

    // Update is called once per frame
    void Update()
    {
      
        //if (Input.anyKeyDown)
      //  {
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
                    else 
                    {
                        inputField.text = "";
                        panel.SetActive(false);
                        objectName.text = "";
                        wordsSuggested.text = "";
                    }
                }
            //}
            
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
        wordsSuggested.text = "TAKE\n" +"BLOW\n" +"TOUCH";
        objectHitName = "torch";
    }

    public void HittingCelldoor()
    {
       
        objectName.text = "The Celldoor";
        wordsSuggested.text = "OPEN\n" +"CLOSE\n" +"";
        objectHitName = "celldoor";
    }
    public void HittingPushSpell()
    {
      
        objectName.text = "WITCH SCREAM";
        wordsSuggested.text = "READ\n";
        objectHitName = "pushspell";

    }
    public void HittingLever1()
    {
       
        objectName.text = "Lever";
        wordsSuggested.text = "Pull Left\n" + "Pull Right\n";
        objectHitName = "lever1";

    }
    public void HittingSpikeLever()
    {

        objectName.text = "Spike Trap";
        wordsSuggested.text = "Pull Left\n" + "Pull Right\n";
        objectHitName = "spikeLever";

    }
    public void HittingChestKey()
    {

        objectName.text = "Key";
        wordsSuggested.text = "Take";
        objectHitName = "chestKey1";

    }
    public void HittingChest()
    {

        objectName.text = "Chest";
        wordsSuggested.text = "Open";
        objectHitName = "chest1";

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
        if(objectHitName == "pushspell" && action == "read")
        {
            pushSymbol.SetActive(true);
            iLearnedPush = true;
            //hit.transform.gameObject.SetActive(false);
            GameObject pushS;
            pushS = GameObject.FindGameObjectWithTag("pushspell");
            pushS.SetActive(false);
        }
        else if (objectHitName == "celldoor")
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
                //cellDoorScript.pushIsApplied = true;
                StartCoroutine(BustDoor());
                AudioSource.PlayClipAtPoint(pushSpellSound, transform.position);
                
            }
        }
        else if (objectHitName == "lever1")
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
        else if (objectHitName == "spikeLever")
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
                    //StartCoroutine(MonsterAppear()); 
                    chest.GetComponent<Animation>().Play("ChestAnim");
                    AudioSource.PlayClipAtPoint(openChestSound, transform.position);
                    //// to activate the alarm trap
                    GameObject alarmTrap = GameObject.Find("Alarm Trap");
                    AlarmTrap alarmTrapScript = alarmTrap.GetComponent<AlarmTrap>();
                    alarmTrapScript.actvateTrap = true;
                }
            }
        }
        if (objectHitName == "torch")
        {
            
            if (action == "take")
            {
                hit.transform.gameObject.SetActive(false);
                playeTorch.SetActive(true);
                Debug.Log("torch taken");
                AudioSource.PlayClipAtPoint(takeTorchSound, transform.position);
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
            hit.transform.tag == "chest1" ||
            hit.transform.tag == "pushspell")
        {
            theCursorWhenHitting.SetActive(true);
        }
        else
            theCursorWhenHitting.SetActive(false);
    }
    IEnumerator BustDoor()
    {
        GameObject tutorialDoor = GameObject.Find("Tutorial Door");
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
