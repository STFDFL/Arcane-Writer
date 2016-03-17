using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Characters.FirstPerson;

public class TextCombatScript : MonoBehaviour
{

    public FirstPersonController FPSC;
    public bool inCombat = false;
	public int playerAttackNumber;
	public int AIAttackNumber;
	public float playerHealth;
	public float AIHealth;
	public bool playerTurn;
	public bool AITurn;
	public float playerTurnTimer;
	public int playerTurnTimerReset;
	private int playerHitChance;
	private int lastPlayerHitChance;
	private int AIHitChance;
	private int lastAIHitChance;
	public Slider playerHealthBar;
	public Slider AIHealthBar;
	public Slider timerBar;
	private bool timerbarActive;
    public GameObject combatUI;
    public Enemy enemy;
    public bool playerDefence;
    public bool AIDefence;

    
    public int playerDamage1;
    public int playerDamage2;
    public int playerDamage3;
    public int playerDamage4;
    public int playerDamage5;
    public int playerDamage6;
    public int playerDamage7;
    public int playerDamage8;

    //input variables
    public string combatAction;
    public InputField inputField;
    // sounds variables
    public GameObject battleSound;

    //spells variables
    public GameObject snakeVomit;
    public AudioClip snakeVomitSound;
    

    public void Start ()
    {
        AIDefence = false;
        playerDefence = false;
        playerHealth = 100;
		AIHealth = 100;
		playerTurn = true;
		playerTurnTimerReset = Mathf.RoundToInt (playerTurnTimer);
		playerHealthBar.maxValue = 100f;
		playerHealthBar.minValue = 0;
		AIHealthBar.maxValue = 100;
		AIHealthBar.minValue = 0;
		timerBar.maxValue = playerTurnTimer;
		timerBar.minValue = 0;
		playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
		AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
		timerBar.value = Mathf.MoveTowards (playerTurnTimer, 100.0f, 0.15f);
        enemy = enemy.GetComponent<Enemy>();
        FPSC = GameObject.FindObjectOfType<FirstPersonController>();
        
    }

	public void Update ()
    {
        combatAction = inputField.text;
        if (inCombat)
        { 
            FPSC.GetComponent<FirstPersonController>().enabled = false;
            combatUI.SetActive(true);
            battleSound.SetActive(true);
            if (playerTurn == true)
            {
                inputField.ActivateInputField();
                Debug.Log("Player turn");
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.LogWarning(combatAction);
                    StopCoroutine(PlayerTurnTimer());
                    playerHitChanceReset:
                    playerHitChance = Random.Range(0, 100);
                    if (playerHitChance == lastPlayerHitChance)
                    {
                        goto playerHitChanceReset;
                    }
                    lastPlayerHitChance = playerHitChance;
                    if (combatAction == "bite")
                    {
                        playerAttackNumber = 1;
                    }
                    else if (combatAction == "kick")
                    {
                        playerAttackNumber = 2;
                    }
                    else if (combatAction == "slap")
                    {
                        playerAttackNumber = 3;
                    }
                    else if (combatAction == "scratch")
                    {
                        playerAttackNumber = 4;
                    }
                    else if (combatAction == "stand still")
                    {
                        playerAttackNumber = 5;
                    }
                    else if (combatAction == "strong punch")
                    {
                        playerAttackNumber = 6;
                    }
                    else if (combatAction == "snake vomit")
                    {
                        playerAttackNumber = 7;
                    }
                    else if (combatAction == "lick your wounds")
                    {
                        playerAttackNumber = 8;
                    }
                    
                    else {
                        playerAttackNumber = 0;
                    }
                    StopCoroutine(PlayerTurnTimer());
                    playerTurnTimer = playerTurnTimerReset;
                    inputField.text = "";
                    StartCoroutine(Wait());
                    
                }
                //			timerbarActive = timerBar.IsActive();

                if (playerTurnTimer > 0 && playerTurn == true)
                {
                    //				StartCoroutine (PlayerTurnTimer ());
                    playerTurnTimer = playerTurnTimer - 1;
                    timerBar.value = Mathf.MoveTowards(playerTurnTimer, 100.0f, 0.5f);

                }
                else if (playerTurnTimer <= 0 && playerTurn == true)
                {
                    //				StopCoroutine (PlayerTurnTimer ());
                    playerTurn = false;
                    playerTurnTimer = playerTurnTimerReset;
                }
                else if (playerTurn == false)
                {
                    //				StopCoroutine (PlayerTurnTimer ());
                    playerTurnTimer = playerTurnTimerReset;
                    playerTurn = false;
                    AITurn = true;
                    playerTurnTimer = playerTurnTimerReset;
                }

            }

            //		StopCoroutine (PlayerTurnTimer ());
            else if (playerTurn == false)
            {
                timerBar.gameObject.SetActive(false);
                //			timerBar.enabled = false;
                AITurn = true;
                //			StopCoroutine (PlayerTurnTimer ());
                AIAttackNumber = Random.Range(1, 3);
            AIHitChanceReset:
                AIHitChance = Random.Range(1, 100);
                if (AIHitChance == lastAIHitChance)
                {
                    goto AIHitChanceReset;
                }
                lastAIHitChance = AIHitChance;
                Debug.LogWarning("AI finished turn");
                playerTurn = true;
                AITurn = false;

                //			StopAllCoroutines ();
                //timerBar.enabled = false;
                Outcome();
                timerBar.gameObject.SetActive(true);
            }
            if (playerHealth < 10 || AIHealth < 10)
            {
                if (playerHealth < 10)
                {
                    Debug.LogError("PLAYER IS DEAD");
                }
                else if (AIHealth < 10)
                {
                    inCombat = false;
                    enemy.isThisAlive = false;
                    combatUI.SetActive(false);
                    battleSound.SetActive(false);
                    FPSC.GetComponent<FirstPersonController>().enabled = true;
                }
                
            }
        } 
	}

	void Outcome(){
		StopCoroutine (PlayerTurnTimer ());
		playerTurnTimer = playerTurnTimerReset;

		if (playerHealth > 9 && AIHealth > 9) {
			//player
			if (playerAttackNumber == 1 ) {
				AIHealth = AIHealth - playerDamage1;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			} else if (playerAttackNumber == 2 ) {
				AIHealth = AIHealth - playerDamage2;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			} else if (playerAttackNumber == 3 ) {
				AIHealth = AIHealth - playerDamage3;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			}
            else if (playerAttackNumber == 4)
            {
                AIHealth = AIHealth - playerDamage4;
                AIHealthBar.value = Mathf.MoveTowards(AIHealth, 100.0f, 0.15f);
            }
            else if (playerAttackNumber == 5)
            {
                AIHealth = AIHealth - playerDamage5;
                AIHealthBar.value = Mathf.MoveTowards(AIHealth, 100.0f, 0.15f);
            }
            else if (playerAttackNumber == 6)
            {
                AIHealth = AIHealth - playerDamage6;
                AIHealthBar.value = Mathf.MoveTowards(AIHealth, 100.0f, 0.15f);
            }
            else if (playerAttackNumber == 7)
            {
                snakeVomit.SetActive(true);
                AudioSource.PlayClipAtPoint(snakeVomitSound, transform.position);
                AIHealth = AIHealth - playerDamage7;
                AIHealthBar.value = Mathf.MoveTowards(AIHealth, 100.0f, 0.15f);
            }
            else if (playerAttackNumber == 8)
            {
                AIHealth = AIHealth - playerDamage8;
                AIHealthBar.value = Mathf.MoveTowards(AIHealth, 100.0f, 0.15f);
                playerHealth = playerHealth + 20;
                playerHealthBar.value = Mathf.MoveTowards(playerHealth, 100.0f, 0.15f);
            }

            else if (playerAttackNumber == 0) {
//				Debug.LogWarning ("player didn't attack");
			}
			else {
//				Debug.LogWarning ("Player attack failed");
			}
			//AI
			if (AIAttackNumber == 1 && AIHitChance > 25) {
				playerHealth = playerHealth - 10;
				playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
			} else if (AIAttackNumber == 2 && AIHitChance > 50) {
				playerHealth = playerHealth - 20;
				playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
			} else if (AIAttackNumber == 3 && AIHitChance > 75) {
				playerHealth = playerHealth - 30;
				playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
			} else {
//				Debug.LogWarning ("AI attack failed");
			}
		}
	}

	IEnumerator Wait(){
//		StopCoroutine (PlayerTurnTimer ());
		timerBar.gameObject.SetActive (false);
//		timerBar.enabled = false;
		Debug.Log("Does this");
		yield return new WaitForSeconds (2);
		playerTurn = false;
        snakeVomit.SetActive(false);
    }
	IEnumerator PlayerTurnTimer(){
		yield return new WaitForSeconds (100000);

//		Debug.Log ("This Happens");
	}
}