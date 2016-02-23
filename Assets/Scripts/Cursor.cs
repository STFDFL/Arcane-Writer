using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Cursor : MonoBehaviour
{

    private Ray ray; // the ray that will be shot
    private RaycastHit hit; // variable to hold the object that is hit
    public Camera playerCamera;
    public InputField inputField;
    public float raycastDistance;
    
    private string action;

    //panel variables
    public GameObject panel;
    public Text objectName;
    public Text wordsSuggested;


    // Use this for initialization
    void Start()
    {
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
                        panel.SetActive(true);
                        inputField.enabled = false;
                        objectName.text = "A Torch";
                        wordsSuggested.text = "TAKE\n" +
                                               "BLOW\n" +
                                                 "TOUCH";
                    }
                    else if (hit.transform.tag == "celldoor")
                    {
                        panel.SetActive(true);
                        inputField.enabled = false;
                        objectName.text = "The Celldoor";
                        wordsSuggested.text = "OPEN\n" +
                                               "CLOSE\n" +
                                                 "";
                    }
                    else 
                    {
                        inputField.enabled = true;
                        inputField.text = "";
                        panel.SetActive(false);
                        objectName.text = "";
                        wordsSuggested.text = "";
                    }
                }
            }
        }
    }
}
