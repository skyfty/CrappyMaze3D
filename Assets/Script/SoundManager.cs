using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public bool isSoundOn;

//	public AudioClip clipClick;
//	public AudioClip clipBack;
//	public AudioClip clipWin;

	public AudioSource sourceClick;
	public AudioSource sourceBack;
	public AudioSource sourceWin;

	// Awake must use here for other object to use the isSoundOn.
	void Awake () {
		if (PlayerPrefs.HasKey ("ButtonSound.isSoundOn")) {
			if (PlayerPrefs.GetInt ("ButtonSound.isSoundOn") == 0) {
				isSoundOn = false;
			} else {
				isSoundOn = true;
			}
		} else {
			isSoundOn = true;
		}
		PlayBack ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isSoundOn) {
			if (sourceWin.isPlaying) {
				sourceBack.Pause ();
			} else {
				if (sourceBack.isPlaying) {
					sourceBack.UnPause ();
				} else {
					sourceBack.Play ();
				}
			}
		}
	}

	public void PlayClick (){
		if (!isSoundOn)
			return;
		sourceClick.Play ();
	}

	public void PlayBack (){
		if (!isSoundOn)
			return;
		sourceBack.Play ();
	}

	public void PlayWin (){
		if (!isSoundOn)
			return;
		sourceWin.Play ();
	}

	public void PauseBack (){
		if (isSoundOn)
			return;
		sourceBack.Pause ();
	}

	public void UnPauseBack (){
		if (!isSoundOn)
			return;
		if (sourceBack.isPlaying) {
			sourceBack.UnPause ();
		} else {
			sourceBack.Play ();
		}
	}

	public void SetIsSound (bool setIsSound) {
		isSoundOn = setIsSound;
		if (isSoundOn) {
			PlayerPrefs.SetInt ("ButtonSound.isSoundOn", 1);
			UnPauseBack ();
		} else {
			PlayerPrefs.SetInt ("ButtonSound.isSoundOn", 0);
			PauseBack ();
		}
	}
}
