using UnityEngine;
using System.Collections;

public class AlarmTrap : MonoBehaviour
{
    public bool actvateTrap;
    public AudioClip alarmSound;
    public AudioClip monsterSound;
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    GameObject spawnPointChosen;
    GameObject enemyChosen;
    int spawnIndex;
    int enemyIndex;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(actvateTrap)
        {
            Debug.Log("Alarm is On");
            AudioSource.PlayClipAtPoint(alarmSound, transform.position);
            spawnIndex = Random.Range(0, spawnPoints.Length);
            enemyIndex = Random.Range(0, enemies.Length);
            spawnPointChosen = spawnPoints[spawnIndex];
            enemyChosen = enemies[enemyIndex];
            enemyChosen.SetActive(true);
            enemyChosen.transform.position = spawnPointChosen.transform.position;
            AudioSource.PlayClipAtPoint(monsterSound, transform.position);
            actvateTrap = false;
        }
	}

}
