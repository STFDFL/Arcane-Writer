using UnityEngine;
using System.Collections;

public class DeactivateMessage : MonoBehaviour {

   public float timeLeft = 5f;
	public float timeLeft2;
    // Use this for initialization
    void Start ()
    {
		timeLeft2 = timeLeft;
	}
   

    // Update is called once per frame
    void Update ()
    {
        if (gameObject.activeSelf)
        {
            timeLeft2 -= Time.deltaTime;
            if (timeLeft2 < 0)
            {
                gameObject.SetActive(false);
				timeLeft2 = timeLeft;
            }
        }
    }
}
