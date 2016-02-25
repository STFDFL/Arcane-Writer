using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public AudioClip inputConfirm;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void PlayInputConfirm()
    {
        AudioSource.PlayClipAtPoint(inputConfirm, transform.position);
    }
}
