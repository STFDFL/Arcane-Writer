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
    public int healthPotInInventory;
    public Text potionsText;
    public Text coinsText;
    public float potionPower;
    GameObject sceneManager;
    TextCombatScript combatManager;
    // Use this for initialization
    void Start ()
    {
        coinsCollected = 0;
        healthPotCollected = 0;
        //healthPotInInventory = 0;
        healthPotUsed = 0;
        sceneManager = GameObject.Find("SceneManager");
        combatManager = sceneManager.GetComponent<TextCombatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerPrefs.SetInt("coinsColl",coinsCollected);
        PlayerPrefs.SetInt("potionsColl",healthPotCollected);
        PlayerPrefs.SetInt("potionsInv", healthPotInInventory);
        potionsText.text = healthPotInInventory.ToString();
        coinsText.text = coinsCollected.ToString();

        if (Input.GetKeyDown(KeyCode.H))
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
    
}
