using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private int coinsCollected;
    [SerializeField]
    private int healthPotCollected;
    [SerializeField]
    private int healthPotUsed;
    [SerializeField]
    private int healthPotInInventory;
    public Text potionsText;
    public Text coinsText;
    // Use this for initialization
    void Start ()
    {
        coinsCollected = 0;
        healthPotCollected = 0;
        healthPotInInventory = 0;
        healthPotUsed = 0;

	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerPrefs.SetInt("coinsColl",coinsCollected);
        PlayerPrefs.SetInt("potionsColl",healthPotCollected);
        PlayerPrefs.SetInt("potionsInv", healthPotInInventory);
        potionsText.text = healthPotInInventory.ToString();
        coinsText.text = coinsCollected.ToString();
    }
    public void PotionFound()
    {
        healthPotCollected++;
        healthPotInInventory++;
        
    }
    public void CoinFound()
    {
        coinsCollected++;
    }
    
}
