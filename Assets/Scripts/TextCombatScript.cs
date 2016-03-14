using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class TextCombatScript : MonoBehaviour {

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
    

	public void Start () {
		playerHealth = 100;
		AIHealth = 100;
		playerTurn = true;
		playerTurnTimerReset = Mathf.RoundToInt (playerTurnTimer);
		playerHealthBar.maxValue = 100;
		playerHealthBar.minValue = 0;
		AIHealthBar.maxValue = 100;
		AIHealthBar.minValue = 0;
		timerBar.maxValue = playerTurnTimer;
		timerBar.minValue = 0;
		playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
		AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
		timerBar.value = Mathf.MoveTowards (playerTurnTimer, 100.0f, 0.15f);

	}

	public void Update ()
    {
        if(inCombat)
        {
            if (playerTurn == true)
            {
                Debug.Log("Player turn");
                if (Input.anyKeyDown)
                {
                    StopCoroutine(PlayerTurnTimer());
                playerHitChanceReset:
                    playerHitChance = Random.Range(0, 100);
                    if (playerHitChance == lastPlayerHitChance)
                    {
                        goto playerHitChanceReset;
                    }
                    lastPlayerHitChance = playerHitChance;
                    if (Input.GetKeyDown("1"))
                    {
                        playerAttackNumber = 1;
                    }
                    else if (Input.GetKeyDown("2"))
                    {
                        playerAttackNumber = 2;
                    }
                    else if (Input.GetKeyDown("3"))
                    {
                        playerAttackNumber = 3;
                    }
                    else if (Input.GetKeyDown("4"))
                    {
                        playerAttackNumber = 4;
                    }
                    else {
                        playerAttackNumber = 0;
                    }
                    StopCoroutine(PlayerTurnTimer());
                    playerTurnTimer = playerTurnTimerReset;
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
                    Debug.LogError("AI WINS");
                }
                else if (AIHealth < 10)
                {
                    Debug.LogError("YOU WIN");
                }
                else {
                    Debug.LogError("Draw");
                }
            }
        } else
        {
            //unlock the camera
        }
	}

	void Outcome(){
		StopCoroutine (PlayerTurnTimer ());
		playerTurnTimer = playerTurnTimerReset;

		if (playerHealth > 9 && AIHealth > 9) {
			//player
			if (playerAttackNumber == 1 && playerHitChance > 25) {
				AIHealth = AIHealth - 10;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			} else if (playerAttackNumber == 2 && playerHitChance > 50) {
				AIHealth = AIHealth - 20;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			} else if (playerAttackNumber == 3 && playerHitChance > 75) {
				AIHealth = AIHealth - 30;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
			} else if (playerAttackNumber == 4) {
				AIHealth = AIHealth - 20;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
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
	}
	IEnumerator PlayerTurnTimer(){
		yield return new WaitForSeconds (100000);

//		Debug.Log ("This Happens");
	}
}