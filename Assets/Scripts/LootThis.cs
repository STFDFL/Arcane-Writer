using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LootThis : MonoBehaviour
{

    public bool isLooted;
    private int lootChance;
   
    public GameObject coinEffect;
    public GameObject potionEffect;
    public GameObject nothingEffect;

	public AudioClip coinLootSound;
	public AudioClip potionLootSound;
	public AudioClip keyLootSound;
	public AudioClip nothingLootSound;

    public bool keyHere;
    //public AudioClip trapSound;
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
        GameObject inventory = GameObject.Find("SceneManager");
        InventoryManager inventoryManager = inventory.GetComponent<InventoryManager>();
        if (isLooted == false)
        {
            if(keyHere == false)
            {
                lootChance = Random.Range(0, 100);
                if ((lootChance >= 0) && (lootChance < 50)) // 50%
                {
                    // nothing found
                    Instantiate(nothingEffect, gameObject.transform.position, gameObject.transform.rotation);
                    isLooted = true;
					AudioSource.PlayClipAtPoint(nothingLootSound, transform.position);
                    Debug.Log("Nothing Found");
                }
                else if ((lootChance >= 50) && (lootChance < 90)) // 40%
                {
                    //potion is found
                    Instantiate(potionEffect, gameObject.transform.position, gameObject.transform.rotation);
                    inventoryManager.PotionFound();
                    isLooted = true;
					AudioSource.PlayClipAtPoint(potionLootSound, transform.position);
                    Debug.Log("Potion Found");
                }
                else if ((lootChance >= 70) && (lootChance < 90)) 
                {
                    //trap is activated
                    Instantiate(nothingEffect, gameObject.transform.position, gameObject.transform.rotation);
                    isLooted = true;
                    Debug.Log("Enemy Found");
                }
                else if ((lootChance >= 90) && (lootChance <= 100)) // 10%
                {
                    Instantiate(coinEffect, gameObject.transform.position, gameObject.transform.rotation);
                    // coin is found
                    inventoryManager.CoinFound();
                    isLooted = true;
					AudioSource.PlayClipAtPoint(coinLootSound, transform.position);
                    Debug.Log("Coin Found");
                }
            } else if(keyHere == true)
            {
                inventoryManager.KeyFound();
                isLooted = true;
				AudioSource.PlayClipAtPoint(keyLootSound, transform.position);
                Debug.Log("Key Found");
            }

          
            
            
        }
       
    }
}
