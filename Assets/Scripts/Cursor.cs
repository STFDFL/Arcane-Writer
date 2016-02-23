using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Cursor : MonoBehaviour
{

    private Ray ray; // the ray that will be shot
    private RaycastHit hit; // variable to hold the object that is hit
    public Camera playerCamera;
    public InputField inputField;

    private string action;

    // Use this for initialization
    void Start()
    {

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
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.tag == "torch")
                    {
                        inputField.enabled = true;
                        //inputField = InputField
                        Debug.Log("torch found!");
                        // A collision was detected please deal with it
                    }
                    else
                    {
                        inputField.enabled = false;
                    }
                }
            }
        }
    }
}
