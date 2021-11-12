using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageImage : MonoBehaviour {

	public GameObject languageManager;

	public GameObject[] languageGameObject;

	//Awake
	void Awake () {
		for (int i = 0; i < languageGameObject.Length; i++) {
			languageGameObject [i].SetActive (false);
		}
		languageGameObject [(int)languageManager.GetComponent<LanguageManager> ().currentLanguage].SetActive (true);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLanguage () {
		for (int i = 0; i < languageGameObject.Length; i++) {
			languageGameObject [i].SetActive (false);
		}
		languageGameObject [(int)languageManager.GetComponent<LanguageManager> ().currentLanguage].SetActive (true);
	}
}
