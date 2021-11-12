using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;

	public Sprite soundOn;
	public Sprite soundOff;
	private bool isSoundOn;

	// Use this for initialization
	void Start () {
		isSoundOn = soundManager.GetComponent<SoundManager> ().isSoundOn;
		if (isSoundOn) {
			this.GetComponent<Image> ().sprite = soundOn;
		} else {
			this.GetComponent<Image> ().sprite = soundOff;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SoundOnOrOff () {
		if (!gameManager.GetComponent<GameManager> ().IsStateMenuWait())
			return;
		isSoundOn = soundManager.GetComponent<SoundManager> ().isSoundOn;
		if (isSoundOn) {
			isSoundOn = false;
			this.GetComponent<Image> ().sprite = soundOff;
			soundManager.GetComponent<SoundManager> ().SetIsSound (false);
		} else {
			isSoundOn = true;
			this.GetComponent<Image> ().sprite = soundOn;
			soundManager.GetComponent<SoundManager> ().SetIsSound (true);
			soundManager.GetComponent<SoundManager> ().PlayClick ();
		}
	}
}
