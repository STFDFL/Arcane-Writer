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
            lootChance = Random.Range(0, 100);
            if((lootChance >= 0) && (lootChance < 40)) // 40%
            {
                // nothing found
                Instantiate(nothingEffect, gameObject.transform.position, gameObject.transform.rotation);
                isLooted = true;
                Debug.Log("Nothing Found");
            }
            else if ((lootChance >= 40) && (lootChance < 70)) // 30%
            {
                //potion is found
                Instantiate(potionEffect, gameObject.transform.position, gameObject.transform.rotation);
                inventoryManager.PotionFound();
                isLooted = true;
                Debug.Log("Potion Found");
            }
            else if ((lootChance >= 70) && (lootChance < 90)) // 20%
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
                Debug.Log("Coin Found");
            }
            
        }
       
    }
}
