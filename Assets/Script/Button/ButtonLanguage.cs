using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLanguage : MonoBehaviour {
	
	public GameObject languageManager;
	public GameObject canvasManager;
	public LanguageManager.Language language;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLanguage (){
		languageManager.GetComponent<LanguageManager> ().SetLanguage (language);
	}
}
