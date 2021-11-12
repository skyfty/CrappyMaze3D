using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour {

	public enum Language
	{
		English = 0,
		French = 1,
		Spanish = 2,
		Portuguese = 3,
		Russian = 4,
		German = 5,
		Chinese = 6,
		Arabic = 7,
		Vietnamese = 8,
		Indonesia = 9,
		Persian = 10,
		Urdu = 11
	}

	private int state;
	private const int STATE_NOT_SET = 0;
	private const int STATE_SETTED = 1;

	public Language currentLanguage = Language.English;
	public GameObject[] languageImageList;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("LanguageManager.currentLanguage")) {
			currentLanguage = (Language)PlayerPrefs.GetInt ("LanguageManager.currentLanguage");				//load
			SetLanguage (currentLanguage);
		} else {
			state = STATE_NOT_SET;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLanguage (Language setLanguage) {
		currentLanguage = setLanguage;
		PlayerPrefs.SetInt ("LanguageManager.currentLanguage",(int)currentLanguage);						//save
		for (int i = 0; i < languageImageList.Length; i++) {
			languageImageList [i].GetComponent<LanguageImage> ().SetLanguage ();
		}
		state = STATE_SETTED;
	}

	public bool IsSetted () {
		if (state == STATE_SETTED) {
			return true;
		}
		return false;
	}
}
