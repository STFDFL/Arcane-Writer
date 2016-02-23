using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{

    private Ray ray; // the ray that will be shot
    private RaycastHit hit; // variable to hold the object that is hit
    public Camera playerCamera;
    public InputField inputField;
    public float raycastDistance;
    
   [HideInInspector]
    public string action;

    //panel variables
    public GameObject panel;
    public Text objectName;
    public Text wordsSuggested;

    CursorLockMode wantedMode;

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        action = inputField.text;
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
                        HittingTorch();
               
                    }
                    else if (hit.transform.tag == "celldoor")
                    {
                        HittingCelldoor();
                    }
                    else if (hit.transform.tag == "pushspell")
                    {
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
        wordsSuggested.text = "TAKE\n" +
                               "BLOW\n" +
                                 "TOUCH";
        inputField.ActivateInputField();
    }

    public void HittingCelldoor()
    {
        panel.SetActive(true);
        objectName.text = "The Celldoor";
        wordsSuggested.text = "OPEN\n" +
                               "CLOSE\n" +
                                 "";
        inputField.ActivateInputField();
    }
    public void HittingPushSpell()
    {
        panel.SetActive(true);
        objectName.text = "The Push Spell";
        wordsSuggested.text = "TAKE\n";
        inputField.ActivateInputField();
    }
    // Apply requested cursor state
    void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }
    
}
