using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Characters.FirstPerson;

public class TextCombatScript4 : MonoBehaviour {

	public FirstPersonController FPSC;
	public bool inCombat = false;
	public float playerHealth;
	public float AIHealth;
	public float playerTurnTimer = 500;
	private int playerTurnTimerReset;
	private float AITurnTimer = 50;
	private int AITurnTimerReset;	
	public Slider playerHealthBar;
	public Slider AIHealthBar;
	public Slider timerBar;
	public int playerAttackNumber;
	private int AIAttackNumber;
	private bool playerTurn;
	private int AIHitChance;
	private int lastAIHitChance;
	private bool timerbarActive;
	public GameObject combatUI;
	public Enemy enemy;
	private float playerDefending;
	private float AIDefending;
	public float playerDefenceValue;
	public float AIDefenceValue;
	private float damageDelt;
	public int[] playerDamage;

	//Guard Attacks
	public int guardAttackMax;
	public int guardAttackMin;
	public int guardHeal;
	//Hellhound
	public int houndAttackMax;
	public int houndAttackMin;
	public int houndSpecialAttackChance;
	public bool houndSpecialAttackActive;
	public int houndSpecialAttackDamage;

	//Banshee
	public int bansheeAttackMax;
	public int bansheeAttackMin;
	public int bansheeMagicAttack;
	public int bansheeSpecialAttackChance;
	public bool bansheeSpecialAttackActive = false;
	//Spider
	public int spiderAttackMax;
	public int spiderAttackMin;
	public int spiderSpecialAttackChance;
	public float spiderSpecialDamagePercentage;

	//input variables
	private string combatAction;
	public InputField inputField;
	//public InputField EnemyDamageRecieved;
	//public InputField PlayerDamageRecieved;
	// sounds variables
	public GameObject battleSound;
	//spells variables
	public GameObject snakeVomit;
	public AudioClip snakeVomitSound;
	//sound varibles
	public AudioClip[] playerHitSound;
	public AudioClip[] enemyHitSound;
	public AudioClip[] enemyAttackSound;
	//camera shake
	//	public  GameObject cameraShaker;
	public GameObject cameraShaker;
	private int AIAttacks;
	private bool combat;
	//	public Animator AIAnimator;

	void Start () {
		playerHealth = 100;
		AIHealth = 100;
		PlayerPrefs.GetFloat ("Player Health");
		playerTurn = true;
		playerTurnTimerReset = Mathf.RoundToInt (playerTurnTimer);
		AITurnTimerReset = Mathf.RoundToInt (AITurnTimer);
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

		//		AIAnimator = enemy.GetComponent<Animator> ();
	}

	void Update () {

		enemy = enemy.GetComponent<Enemy> ();
		combat = enemy.GetComponent<Enemy> ().combatOn;
		combatAction = inputField.text;
		PlayerPrefs.SetFloat ("Player Health", playerHealth);
		if (combat == true) {
			FPSC.GetComponent<FirstPersonController> ().enabled = false;
			enemy.GetComponent<ThirdPersonCharacter> ().enabled = false;
			enemy.GetComponent<AICharacterController> ().enabled = false;
			combatUI.SetActive (true);
			battleSound.SetActive (true);
			//Player Turn
			if (playerTurn == true) {
				inputField.ActivateInputField ();
				if (Input.GetKeyDown (KeyCode.Return) && bansheeSpecialAttackActive == false) {
					if (combatAction == "bite") {
						playerAttackNumber = 1;
					} else if (combatAction == "kick") {
						playerAttackNumber = 2;
					} else if (combatAction == "slap") {
						playerAttackNumber = 3;
					} else if (combatAction == "scratch") {
						playerAttackNumber = 4;
					} else if (combatAction == "stand still") {
						playerAttackNumber = 5;
					} else if (combatAction == "strong punch") {
						playerAttackNumber = 6;
					} else if (combatAction == "snake vomit") {
						playerAttackNumber = 7;
					} else if (combatAction == "lick your wounds") {
						playerAttackNumber = 8;
					} else if (combatAction == "guard") {
						playerAttackNumber = 9;
					} else {
						playerAttackNumber = 0;
					}
					playerTurnTimer = playerTurnTimerReset;
					PlayerTurnOutcome ();
					inputField.text = "";
					timerBar.gameObject.SetActive (false);
					playerTurn = false;
				} else if (bansheeSpecialAttackActive == true) {
					playerAttackNumber = 0;
					playerTurnTimer = playerTurnTimerReset;
					PlayerTurnOutcome ();
					inputField.text = "";
					timerBar.gameObject.SetActive (false);
					playerTurn = false;
					bansheeSpecialAttackActive = false;
				}

				if (playerTurnTimer > 0 && playerTurn == true) {
					playerTurnTimer = playerTurnTimer - 1;
					timerBar.value = Mathf.MoveTowards (playerTurnTimer, 100.0f, 0.5f);
					//					playerTurn = true;
				}
				else if (playerTurnTimer <= 0 && playerTurn == true) {
					playerTurn = false;
					playerTurnTimer = playerTurnTimerReset;
					PlayerTurnOutcome ();
				}
				else if (playerTurn == false) {
					playerTurnTimer = playerTurnTimerReset;
					playerTurnTimer = playerTurnTimerReset;
					PlayerTurnOutcome ();
				}
			}
			//AI Turn
			else if (playerTurn == false) {
				//PlayerDamageRecieved.text = "";
				playerTurnTimer = playerTurnTimer + 1;
				timerBar.gameObject.SetActive (false);
				AIHitChanceReset:
				AIHitChance = Random.Range (1, 100);
				if (AIHitChance == lastAIHitChance) {
					goto AIHitChanceReset;
				}
				lastAIHitChance = AIHitChance;
				if (AITurnTimer > 0) {
					AITurnTimer = AITurnTimer - 1;
				}
				else {
					AITurnOutcome ();
					AITurnTimer = AITurnTimerReset;
					//EnemyDamageRecieved.text = "";
					timerBar.gameObject.SetActive (true);
					playerTurn = true;
				}
			}
		}
		else if (combat == false) {
			FPSC.GetComponent<FirstPersonController> ().enabled = true;
			enemy.GetComponent<ThirdPersonCharacter> ().enabled = true;
			enemy.GetComponent<AICharacterController> ().enabled = true;
		}
		playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
		AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
	}
	void HealthCheck(){
		if (playerHealth < 5 || AIHealth < 5) {
			if (playerHealth <= 0) {
				combat = false;
				SceneManager.LoadScene("Owen's Combat Testing Scene");
			}
			else if (AIHealth <= 0) {
				combat = false;
				enemy.isThisAlive = false;
				combatUI.SetActive(false);
				battleSound.SetActive(false);
				FPSC.GetComponent<FirstPersonController>().enabled = true;
			}
		}
	}
	void PlayerTurnOutcome(){
		if (enemy.tag == "Guard" || enemy.tag == "Banshee") {
			AIAttacks = 3;
		}
		if (enemy.tag == "Spider" || enemy.tag == "Hellhound") {
			AIAttacks = 2;
		}

		AIAttackNumber = Random.Range (1, AIAttacks);
		if (AIAttackNumber == 2 && enemy.tag == "Guard") {
			AIDefending = AIDefenceValue;
		}
		else {
			AIDefending = 1;
		}
		playerTurnTimer = playerTurnTimerReset;

		if (houndSpecialAttackActive == true && playerAttackNumber != 0 || playerAttackNumber != 5) {
			float deactivate = Random.Range (1, 2);
			if (deactivate == 2) {
				houndSpecialAttackActive = false;
			} else {
				playerHealth = playerHealth - houndSpecialAttackDamage;
				//EnemyDamageRecieved.text = ("-" + houndSpecialAttackDamage.ToString());
			}
		}

		if (playerHealth > 4) {
			if (playerAttackNumber == 1) {
				AIHealth = AIHealth - playerDamage[0] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[0] * AIDefending;
			}
			else if (playerAttackNumber == 2) {
				AIHealth = AIHealth - playerDamage[1] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[1] * AIDefending;
			}
			else if (playerAttackNumber == 3) {
				AIHealth = AIHealth - playerDamage[2] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[2] * AIDefending;
			}
			else if (playerAttackNumber == 4) {
				AIHealth = AIHealth - playerDamage[3] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[3] * AIDefending;
			}
			else if (playerAttackNumber == 5) {
				AIHealth = AIHealth - playerDamage[4] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[4] * AIDefending;
			}
			else if (playerAttackNumber == 6) {
				AIHealth = AIHealth - playerDamage[5] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[5] * AIDefending;
			}
			else if (playerAttackNumber == 7) {
				snakeVomit.SetActive (true);
				AudioSource.PlayClipAtPoint (snakeVomitSound, transform.position);
				AIHealth = AIHealth - playerDamage[6] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[6] * AIDefending;
			}
			else if (playerAttackNumber == 8) {
				AIHealth = AIHealth - playerDamage[7] * AIDefending;
				AIHealthBar.value = Mathf.MoveTowards (AIHealth, 100.0f, 0.15f);
				playerHealth = playerHealth + 20;
				playerHealthBar.value = Mathf.MoveTowards (playerHealth, 100.0f, 0.15f);
				playerDefending = 1;
				//				HitEnemySound ();
				damageDelt = playerDamage[7] * AIDefending;
			}
			else if (playerAttackNumber == 9) {
				playerDefending = playerDefenceValue;
			} 
			else {
				playerDefending = 1;
			}
		}
		//EnemyDamageRecieved.text = ("-" + damageDelt.ToString());
		damageDelt = 0;
		HealthCheck ();
	}
	void AITurnOutcome(){

		if (enemy.tag == "Guard") {
			if (AIAttackNumber == 1 && AIHitChance > 25) {
				int AIDamage = Random.Range (guardAttackMin, guardAttackMax);
				playerHealth = playerHealth - AIDamage * playerDefending;
				AIDefending = 1;
				//			HitPlayerSound ();
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
				damageDelt = AIDamage * playerDefending;
			}
			else if (AIAttackNumber == 3 && AIHitChance > 50) {
				if (AIHealth < 30) {
					AIHealth = AIHealth + guardHeal;
					AIDefending = 1;
				}
			}
		}

		if (enemy.tag == "Banshee") {
			if (AIAttackNumber == 1 && AIHitChance > 25) {
				int AIDamage = Random.Range (bansheeAttackMin, bansheeAttackMax);
				playerHealth = playerHealth - AIDamage * playerDefending;
				AIDefending = 1;
				//			HitPlayerSound ();
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
				damageDelt = AIDamage * playerDefending;
			}
			if (AIAttackNumber == 2 && AIHitChance > 50) {
				if (AIHealth < 40) {
					int AIDamage = bansheeMagicAttack;
					playerHealth = playerHealth - AIDamage * playerDefending;
					AIDefending = 1;
					//			HitPlayerSound ();
					cameraShaker.GetComponent<CameraShake> ().enabled = true;
					damageDelt = AIDamage * playerDefending;
				}
			}
			if (AIAttackNumber == 3 && AIHitChance > bansheeSpecialAttackChance) {
				bansheeSpecialAttackActive = true;
				if (bansheeSpecialAttackChance < 98) {
					bansheeSpecialAttackChance = bansheeSpecialAttackChance + 10;
				} else {
					bansheeSpecialAttackChance = 99;
				}
				AIDefending = 1;
				//			HitPlayerSound ();
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
			}
		}

		if (enemy.tag == "Spider") {
			if (AIAttackNumber == 1) {
				int AIDamage = Random.Range (spiderAttackMin, spiderAttackMax);
				playerHealth = playerHealth - AIDamage * playerDefending;
				AIDefending = 1;
				//			HitPlayerSound ();
				enemy.GetComponent<Animation>().Play("SpiderAttack");
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
				damageDelt = AIDamage * playerDefending;
			} else if (AIAttackNumber == 2) {
				if (AIHealth < 45) {
					playerHealth = playerHealth - playerHealth * 0.05f;
					enemy.GetComponent<Animation>().Play("SpiderAttack");
					cameraShaker.GetComponent<CameraShake> ().enabled = true;
				}
			}
		}

		if (enemy.tag == "Hellhound") {
			if (AIAttackNumber == 1) {
				int AIDamage = Random.Range (houndAttackMin, houndAttackMax);
				playerHealth = playerHealth - AIDamage * playerDefending;
				AIDefending = 1;
				//			HitPlayerSound ();
				enemy.GetComponent<Animation>().Play("HellhoundAttack");
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
				damageDelt = AIDamage * playerDefending;
			} else if (AIAttackNumber == 2 && AIHitChance > houndSpecialAttackChance) {
				houndSpecialAttackActive = true;
				enemy.GetComponent<Animation>().Play("HellhoundAttack");
				cameraShaker.GetComponent<CameraShake> ().enabled = true;
			}
		}
		//PlayerDamageRecieved.text = ("-" + damageDelt.ToString());
		damageDelt = 0;
		HealthCheck ();
	}
	void HitPlayerSound(){
		int length = playerHitSound.Length;
		int chosenSound = Random.Range (0, length);
		AudioSource.PlayClipAtPoint(playerHitSound[chosenSound], gameObject.transform.position);
	}
	void HitEnemySound(){
		int length = enemyHitSound.Length;
		int chosenSound = Random.Range (0, length);
		AudioSource.PlayClipAtPoint(enemyHitSound[chosenSound], gameObject.transform.position);
	}
	void EnemyAttackSound(){
		int length = enemyAttackSound.Length;
		int chosenSound = Random.Range (0, length);
		AudioSource.PlayClipAtPoint(enemyAttackSound[chosenSound], gameObject.transform.position);
	}
}