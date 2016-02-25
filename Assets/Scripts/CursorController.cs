using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    
    
    //raycast
    private Ray ray; // the ray that will be shot
    private RaycastHit hit; // variable to hold the object that is hit
    public Camera playerCamera;
    public InputField inputField;
    public float raycastDistance;
    
    //cursor
    CursorLockMode wantedMode;

    //the action wrote by the player
   // [HideInInspector]
    public string action;
    //the object hit by the raycast to check against the action chose by the player
    //[HideInInspector]
    public string objectHitName;
    

    //input panel and suggestions variables
    public GameObject panel;
    public Text objectName;
    public Text wordsSuggested;

    //sounds
    public AudioClip inputPopUp;
    public AudioClip pushSpellSound;
    //spells
    public GameObject pushSymbol;
    private bool iLearnedPush = false;

    //messages popping up
    public GameObject itsLocked;
    public GameObject itsAlreadyClosed;

    
    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            // The Vector2 class holds the position for a point with only x and y coordinates
            // The center of the screen is calculated by dividing the width and height by half
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            // The method ScreenPointToRay needs to be called from a camera
            // Since we are using the MainCamera of our scene we can have access to it using the Camera.main
            ray = playerCamera.ScreenPointToRay(screenCenterPoint);
            Debug.DrawRay(playerCamera.transform.position, screenCenterPoint, Color.green);
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (Input.GetMouseButtonDown(0))
                {
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
                    else 
                    {
                        inputField.text = "";
                        panel.SetActive(false);
                        objectName.text = "";
                        wordsSuggested.text = "";
                    }
                }
            }
        }
        // Release cursor on Esc keypress
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = wantedMode = CursorLockMode.None;
            SetCursorState();
        }
        // Lock cursor on M keypress
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = wantedMode = CursorLockMode.Locked;
            SetCursorState();
        }
            
    }
    public void HittingTorch()
    {
        panel.SetActive(true);
        objectName.text = "A Torch";
        wordsSuggested.text = "TAKE\n" +"BLOW\n" +"TOUCH";
        inputField.ActivateInputField();
        objectHitName = "torch";
    }

    public void HittingCelldoor()
    {
        panel.SetActive(true);
        objectName.text = "The Celldoor";
        wordsSuggested.text = "OPEN\n" +"CLOSE\n" +"";
        inputField.ActivateInputField();
        objectHitName = "celldoor";
    }
    public void HittingPushSpell()
    {
        panel.SetActive(true);
        objectName.text = "Push Spell";
        wordsSuggested.text = "READ\n";
        inputField.ActivateInputField();
        objectHitName = "pushspell";

    }
    // Apply requested cursor state
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
    }
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
        else if (objectHitName == "celldoor" && action == "open")
        {
            itsLocked.SetActive(true);
        }
        else if (objectHitName == "celldoor" && action == "close")
        {
            itsAlreadyClosed.SetActive(true);
        }
        else if (objectHitName == "celldoor" && action == "push" && iLearnedPush == true)
        {
            GameObject tutorialDoor = GameObject.Find("Tutorial Door");
            CellDoor cellDoorScript = tutorialDoor.GetComponent<CellDoor>();
            cellDoorScript.pushIsApplied = true;
            AudioSource.PlayClipAtPoint(pushSpellSound, transform.position);
        }
    }
}
