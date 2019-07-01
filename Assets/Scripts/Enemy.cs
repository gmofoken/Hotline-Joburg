using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Player player;

	public GameObject enemy;
	public GameObject projectile;

	public Vector3 playerPosition;
	public Vector3 enemyPosition;

	public static float enemyY;
	public static float enemyX;

	bool HeroSpotted;
	int movement;

	float timer;
	float rateOfFire;
	float bulletExpire;

	void Start () {
		movement = Random.Range (1, 4);
		timer = 0;

	//	GenerateEnemy ();

		HeroSpotted = false;
	
	}

	void weaponShoot(){
		bulletExpire += Time.deltaTime;
		rateOfFire += Time.deltaTime;

		if (rateOfFire > 3) {
			GameObject projectile_clone = Instantiate (projectile, new Vector3 (getEnemyPosition ().x, getEnemyPosition ().y, getEnemyPosition ().z), Quaternion.identity);
			projectile.transform.position = Vector3.MoveTowards (transform.position, getplayerPosition(), 5f * Time.deltaTime);
			rateOfFire = 0;
		
			if (bulletExpire >= 5 && player.playerHit == false) {
				GameObject.Destroy (projectile_clone);
				Debug.Log (bulletExpire);
				bulletExpire = 0;
			} else if (player.playerHit == true)
				GameObject.Destroy (projectile_clone);
		}
	}

	// Update is called once per frame
	void Update () {

		if(Vector3.Distance(getEnemyPosition(),  getplayerPosition()) < 3)
		{
			// Player is in your radius
			HeroSpotted = true;
		}

		if (HeroSpotted == false)
			randomMovement ();
		else {
			moveEnemy ();
			weaponShoot ();
		}
		EnemyTouchPlayer ();
	}

	void EnemyTouchPlayer (){
		if (Vector3.Distance (getEnemyPosition (), getplayerPosition ()) < 0.5f) {
			player.playerHit = true;				
		}
	}

	void randomMovement(){
		EnemyPatrol (movement);
		}

	void EnemyPatrol (int movement){
		switch (movement) {
		case 1:
			EnemyPatrolStill ();
			break;
		case 2:
			EnemyPatrolPace ();
			break;
		case 3:
			EnemyPatrolRoom ();
			break; 
		}
	}

	//########################### Start Hero Spotted ###################################

	Vector3	getEnemyPosition(){
		string objectName = enemy.name;
		enemyPosition =  GameObject.Find(objectName).transform.position;
		return enemyPosition;
	} 
	Vector3	getplayerPosition(){
		playerPosition =  GameObject.Find("Player").transform.position;
		return playerPosition;
	}

	void moveEnemy(){
		facePlayer ();

		if (getEnemyPosition() != getplayerPosition()) 
			transform.position = Vector3.MoveTowards(transform.position, playerPosition ,  2f * Time.deltaTime);

		enemyX = enemy.transform.position.x;
		enemyY = enemy.transform.position.y;
	}	

	void facePlayer(){
		getplayerPosition ();

		Vector2 direction = new Vector2 (
			playerPosition.x - transform.position.x,
			playerPosition.y - transform.position.y
		);

		transform.up = direction;
	}

	//########################### end Hero Spotted #####################################

	//########################### Start Enemy Patrol ###################################

	void EnemyPatrolStill(){
			transform.localPosition += new Vector3(0, 0, 0);
		Debug.Log ("STILL");
	}
	void EnemyPatrolPace(){
		Debug.Log ("PACE");
		timer += Time.deltaTime;
		if (timer < 5) {
			transform.localPosition += new Vector3 (0.02f, 0, 0);

			Vector2 direction = new Vector2 (
				enemyPosition.x + transform.position.x,
				enemyPosition.y + transform.position.y
			);

			transform.up = direction;


		} else if (timer >= 5 && timer < 10) {



			transform.localPosition += new Vector3 (-0.02f, 0, 0);

		/*	Vector2 direction = new Vector2 (
				enemyPosition.x - transform.position.x,
				enemyPosition.y - transform.position.y
			);

			transform.up = direction;
*/
		}
		if (timer >= 10) {
			timer = 0;
			EnemyPatrolPace ();
		}
	}

	void EnemyPatrolRoom(){
		Debug.Log ("ROOM");
		timer += Time.deltaTime;
		if (timer < 5) {
			transform.localPosition += new Vector3 (0.02f, 0, 0);


		} else if (timer >= 5 && timer < 8) {
			
			transform.localPosition += new Vector3 (0, -0.02f, 0);

		} else if (timer >= 8 && timer < 13) {
			
			transform.localPosition += new Vector3 (-0.02f, 0.0f, 0);


		} else if (timer >= 13 && timer < 16) {
			
			transform.localPosition += new Vector3 (0, 0.02f, 0);

		}

		if (timer >= 16) {
			timer = 0;
			EnemyPatrolRoom ();
		}
		//########################### End Enemy Patrol ###################################
	}
}