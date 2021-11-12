using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAbout : MonoBehaviour {

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

	public void ShowAbout () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		imageBack.SetActive (true);
		imageAbout.SetActive (true);
		buttonAboutReturn.SetActive (true);
		soundManager.GetComponent<SoundManager> ().PlayClick ();
	}
}
