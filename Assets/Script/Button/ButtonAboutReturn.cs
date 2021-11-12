using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAboutReturn : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;
	public GameObject imageBack;
	public GameObject imageAbout;
	public GameObject buttonAboutReturn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HideAbout () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		imageBack.SetActive (false);
		imageAbout.SetActive (false);
		buttonAboutReturn.SetActive (false);
		soundManager.GetComponent<SoundManager> ().PlayClick ();
	}
}
