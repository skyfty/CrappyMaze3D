using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelp : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;
	public GameObject imageBack;
	public GameObject imageHelp;
	public GameObject buttonHelpReturn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowHelp () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		imageBack.SetActive (true);
		imageHelp.SetActive (true);
		buttonHelpReturn.SetActive (true);
		soundManager.GetComponent<SoundManager> ().PlayClick ();
	}
}
