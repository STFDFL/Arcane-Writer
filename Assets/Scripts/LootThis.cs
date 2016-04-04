using UnityEngine;
using System.Collections;

public class LootThis : MonoBehaviour
{

    private bool isLooted;
    private int lootChance;
    public AudioClip trapSound;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void Loot()
    {
        if(isLooted == false)
        {
            lootChance = Random.Range(0, 100);
            if((lootChance >= 0) && (lootChance < 40)) // 40%
            {
                // nothing found
                isLooted = true;
            }
            else if ((lootChance >= 40) && (lootChance < 70)) // 30%
            {
                //potion is found
                AudioSource.PlayClipAtPoint(trapSound, transform.position);
                isLooted = true;
            }
            else if ((lootChance >= 70) && (lootChance < 90)) // 20%
            {
                //trap is activated
                
                isLooted = true;
            }
            else if ((lootChance >= 90) && (lootChance <= 100)) // 10%
            {
                // coin is found

                isLooted = true;
            }
            
        }
       
    }
}
