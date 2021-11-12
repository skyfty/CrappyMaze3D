using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitGame () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		soundManager.GetComponent<SoundManager> ().PlayClick ();
		Application.Quit ();
	}
}
