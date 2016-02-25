using UnityEngine;
using System.Collections;

public class DeactivateMessage : MonoBehaviour {

   public float timeLeft = 5f;
    // Use this for initialization
    void Start ()
    {
	
	}
   

    // Update is called once per frame
    void Update ()
    {
        if (gameObject.activeSelf)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
