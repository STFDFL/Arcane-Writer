using UnityEngine;
using System.Collections;

public class CrushingWallsTrap : MonoBehaviour {
    public bool playerActivateTrap = false;
    public float speed;
    public GameObject pressurePlate;
    public GameObject leftWalls;
    public GameObject rightWalls;
    public bool wallsAreMoving = false;
    public Transform leftWallsT;
    public Transform rightWallsT;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distL = Vector3.Distance(leftWallsT.position, transform.position);
        float distR = Vector3.Distance(rightWallsT.position, transform.position);
        if (wallsAreMoving == true)
        {
            leftWalls.transform.Translate(Vector3.right * Time.deltaTime * speed);
            rightWalls.transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (leftWallsT && rightWallsT)
            {
               
                Debug.Log("distL:"+distL);
                Debug.Log("distR:"+distR);
                if (distL <= 0.75f && distR <= 0.75f)
                {
                    wallsAreMoving = false;
                }
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player enter!");
            pressurePlate.transform.Translate(Vector3.down * Time.deltaTime * speed);
            playerActivateTrap = true;
            wallsAreMoving = true;
        }
    }
}
