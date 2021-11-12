using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelpReturn : MonoBehaviour {
	
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

	public void HideHelp () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		imageBack.SetActive (false);
		imageHelp.SetActive (false);
		buttonHelpReturn.SetActive (false);
		soundManager.GetComponent<SoundManager> ().PlayClick ();
	}
}
