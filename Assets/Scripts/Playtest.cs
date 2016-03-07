using UnityEngine;
using System.Collections;

public class Playtest : MonoBehaviour
{
    public bool fromVeryBeginning;
    public GameObject player;
    public GameObject spawnBeginningLocation;
    public GameObject spawnTestLocation;
    
	// Use this for initialization
	void Start ()
    {
        if(fromVeryBeginning)
        {
            player.transform.position = spawnBeginningLocation.transform.position;
        }
        else if (!fromVeryBeginning)
        {
            player.transform.position = spawnTestLocation.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
    }
}
