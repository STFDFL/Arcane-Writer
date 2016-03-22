using UnityEngine;
using System.Collections;

public class SecretPassage : MonoBehaviour {

    public GameObject wall;
    public AudioClip wallSound;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OpenWall();
        }
    }
    public void OpenWall()
    {
        Debug.Log("wall is opening");
        wall.GetComponent<Animation>().Play("openPassage");
        AudioSource.PlayClipAtPoint(wallSound, transform.position);
    }
}
