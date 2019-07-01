using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
	
	public GameObject PauseMenu;
	public bool isGameRunning;
	public GameObject Canvas;
	public GameObject runGame;

	// Use this for initialization
	void Start () {
		PauseMenu.SetActive(false);
		isGameRunning = true;
		runGame.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (Player.playerX, Player.playerY, -10f);
		pauseState ();
	}

	public void pauseState(){
		PauseMenu.transform.localPosition = new Vector3 (Player.playerX, Player.playerY, 0);
		Canvas.transform.localPosition = new Vector3 (Player.playerX, Player.playerY, 0);
		if (Input.GetKey (KeyCode.P)) {
			if (isGameRunning == true) {
				PauseMenu.SetActive (true);
				isGameRunning = false;
				runGame.SetActive(false);
			} else {
				PauseMenu.SetActive (false);
				isGameRunning = true;
				runGame.SetActive(true);
			}
		}


	}

	public void resume(){
		if (isGameRunning == true) {
			PauseMenu.SetActive (false);
		}
	}
}
