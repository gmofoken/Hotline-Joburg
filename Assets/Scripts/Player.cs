using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public GameObject weapon;
	public GameObject temp;
	public Vector3 mousePosition;
	public GameObject cursor;
	public GameObject player;
	public GameObject Win;
	public GameObject Lose;
	public static float playerY;
	public static float playerX;
	public Cam cam;
	float timer;

	public bool playerHit;
	// Use this for initialization
	void Start () {
		Win.SetActive(false);
		Lose.SetActive(false);
		playerHit = false;
		timer = 0;
	}
		
	// Update is called once per frame
	void Update () {
		if (playerY < -12.3863f) {
			Win.SetActive (true);
			timer += Time.deltaTime;
			if (timer >= 2)
				SceneManager.LoadScene (0);
		} else if (playerHit == true) {
			timer += Time.deltaTime;
			Lose.SetActive (true);
			if (timer >= 2)
				SceneManager.LoadScene (0);
		}
		
		else if (cam.isGameRunning == true) {
			moveCursor ();
			movePlayer ();
		}
	}

	void movePlayer(){
		faceCursor ();

		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
			player.transform.position += new Vector3 (0, 0.05f, 0);

		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
			player.transform.position += new Vector3 (0, -0.05f, 0);

		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
			player.transform.position += new Vector3 (-0.05f, 0, 0);

		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
			player.transform.position += new Vector3 (0.05f, 0, 0);


		playerX = player.transform.position.x;
		playerY = player.transform.position.y;
	}

	void faceCursor(){
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Vector2 direction = new Vector2 (
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

		transform.up = direction;
	}

	void moveCursor(){
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		cursor.transform.localPosition = new Vector2 (mousePosition.x, mousePosition.y);
	}

	void pickUpWeapon(){
		if (Input.GetKey (KeyCode.E))
			GameObject.Destroy (temp);
			
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Weapon")) {
			temp = GameObject.Find(other.name);;
			pickUpWeapon ();
		}
	}
}
