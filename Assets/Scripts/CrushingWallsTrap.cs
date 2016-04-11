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

	public AudioClip movingWallsSound;
	public AudioClip stopWallsSound;
	public AudioClip pPSound;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		float distL = Vector3.Distance(leftWallsT.position, pressurePlate.transform.position);
		float distR = Vector3.Distance(rightWallsT.position, pressurePlate.transform.position);
        if (wallsAreMoving == true)
        {
            leftWalls.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            rightWalls.transform.Translate(Vector3.back * Time.deltaTime * speed);
            if (leftWallsT && rightWallsT)
            {
				AudioSource.PlayClipAtPoint(movingWallsSound, transform.position);
                Debug.Log("distL:"+distL);
                Debug.Log("distR:"+distR);
                if (distL >= 5.1f && distR >= 4.6f)
                {
                    wallsAreMoving = false;
					AudioSource.PlayClipAtPoint(stopWallsSound, transform.position);
                }
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			AudioSource.PlayClipAtPoint(pPSound, transform.position);
            Debug.Log("player enter!");
            pressurePlate.transform.Translate(Vector3.down * Time.deltaTime * speed);
            playerActivateTrap = true;
            wallsAreMoving = true;
        }
    }
}
