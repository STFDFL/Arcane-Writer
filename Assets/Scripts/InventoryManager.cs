using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    
   public int keysCollected;

    private int coinsCollected;

    private int healthPotCollected;

    private int healthPotUsed;
    public int healthPotInInventory;
    public Text potionsText;
    public Text coinsText;
    public Text keysText;
    public float potionPower;
    GameObject sceneManager;
    TextCombatScript combatManager;
	private int zero = 0;

	public bool thisIsTutorial = false;
    // Use this for initialization
    void Start ()
    {
		if (thisIsTutorial) {
		
			PlayerPrefs.SetInt("keysColl", zero);
			PlayerPrefs.SetInt("coinsColl",zero);
			PlayerPrefs.SetInt("potionsColl",zero);
			PlayerPrefs.SetInt("potionsInv", zero);
		}
			

		healthPotInInventory = PlayerPrefs.GetInt ("potionsInv");
        sceneManager = GameObject.Find("SceneManager");
        combatManager = sceneManager.GetComponent<TextCombatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerPrefs.SetInt("keysColl", keysCollected);
        PlayerPrefs.SetInt("coinsColl",coinsCollected);
        PlayerPrefs.SetInt("potionsColl",healthPotCollected);
        PlayerPrefs.SetInt("potionsInv", healthPotInInventory);
        potionsText.text = healthPotInInventory.ToString();
        coinsText.text = coinsCollected.ToString();
        keysText.text = keysCollected.ToString();
		healthPotInInventory = PlayerPrefs.GetInt ("potionsInv");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(healthPotInInventory >0)
            {
                combatManager.playerHealth = combatManager.playerHealth + potionPower;
                combatManager.playerHealthBar.value = Mathf.MoveTowards(combatManager.playerHealth, 100.0f, 0.15f);
                healthPotUsed++;
                healthPotInInventory--;
            }
        }
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
    public void KeyFound()
    {
        keysCollected++;
    }
}
