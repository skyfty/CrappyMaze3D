using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelManager : MonoBehaviour {

	public float buttonInterval;
	public int currentLevel;
	public int finishLevel;
	public GameObject[] buttonLevel;
	public GameObject textCountCurrent;
	public GameObject textCountMaximun;

	private float textCountCurrentRecordPositionX;
	private float textCountMaximunRecordPositionX;

	// Use this for initialization
	void Start () {
		textCountCurrentRecordPositionX = textCountCurrent.transform.localPosition.x;
		textCountMaximunRecordPositionX = textCountMaximun.transform.localPosition.x;
		PrepareButton ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLevel(int setCurrentLevel, int setFinishLevel) {
		currentLevel = setCurrentLevel;
		if (currentLevel >=  buttonLevel.Length) {
			currentLevel = buttonLevel.Length - 1;
		}
		finishLevel = setFinishLevel;
	}

	public void SetButtonLevel () {
		for (int i = 0; i < buttonLevel.Length; i++) {
			if (buttonLevel [i].GetComponent<ButtonLevel> ().number <= finishLevel) {
				buttonLevel [i].GetComponent<ButtonLevel> ().SetFinish ();
			} else {
				buttonLevel [i].GetComponent<ButtonLevel> ().SetLock ();
			}
			if (buttonLevel [i].GetComponent<ButtonLevel> ().number == finishLevel + 1) {
				buttonLevel [i].GetComponent<ButtonLevel> ().SetCurrent ();
			}

		}
		textCountCurrent.gameObject.GetComponent<Text> ().text = (finishLevel+1).ToString();
		textCountMaximun.gameObject.GetComponent<Text> ().text = " / "+(buttonLevel.Length).ToString();
	}

	public void PrepareMoveIn () {
		PrepareButton ();
		for (int i = 0; i < 5; i++) {
			if (((i + currentLevel - 2) >= 0) && ((i + currentLevel - 2) < buttonLevel.Length)) {
				buttonLevel [(i + currentLevel - 2)].GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			}
		}
		textCountCurrent.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
		textCountMaximun.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
	}

	public void PrepareMoveOut () {
		PrepareButton ();
		for (int i = 0; i < 5; i++) {
			if (((i + currentLevel - 2) >= 0) && ((i + currentLevel - 2) < buttonLevel.Length)) {
				buttonLevel [(i + currentLevel - 2)].GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
			}
		}
		textCountCurrent.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
		textCountMaximun.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
	}

	public void KeepMoveIn () {
		for (int i = 0; i < buttonLevel.Length; i++) {
			if (buttonLevel [i]) {
				buttonLevel [i].GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			}
		}
		textCountCurrent.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
		textCountMaximun.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
	}

	public void KeepMoveOut () {
		for (int i = 0; i < buttonLevel.Length; i++) {
			if (buttonLevel [i]) {
				buttonLevel [i].GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			}
		}
		textCountCurrent.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
		textCountMaximun.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
	}

	public bool IsFinishMoveIn () {
		for (int i = 0; i < 5; i++) {
			if (((i + currentLevel - 2) >= 0) && ((i + currentLevel - 2) < buttonLevel.Length)) {
				if (buttonLevel [(i + currentLevel - 2)].GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () == false)
					return false;
			}
		}
		if (!textCountCurrent.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn ())
			return false;
		if (!textCountMaximun.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn ())
			return false;
		return true;
	}

	public bool IsFinishMoveOut () {
		for (int i = 0; i < 5; i++) {
			if (((i + currentLevel - 2) >= 0) && ((i + currentLevel - 2) < buttonLevel.Length)) {
				if (buttonLevel [(i + currentLevel - 2)].GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () == false)
					return false;
			}
		}
		if (!textCountCurrent.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut ())
			return false;
		if (!textCountMaximun.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut ())
			return false;
		return true;
	}

	public void PrepareButton () {
		for (int i = 0; i < buttonLevel.Length; i++) {
			buttonLevel [i].SetActive (true);
			buttonLevel [i].GetComponent<RectTransform>().localPosition = new Vector3(0,-buttonInterval*(i-currentLevel),0);
			buttonLevel [i].SetActive (false);
		}
		int tempFirstButtonLevelNumber = -1;
		for (int i = 0; i < 5; i++) {
			if (((i + currentLevel - 2) >= 0) && ((i + currentLevel - 2) < buttonLevel.Length)) {
				if (tempFirstButtonLevelNumber == -1)
					tempFirstButtonLevelNumber = (i + currentLevel - 2);
				buttonLevel [(i + currentLevel - 2)].SetActive (true);
				buttonLevel [(i + currentLevel - 2)].GetComponent<EffectMoveInOut2D> ().RecordPosition ();			//record new position
			}
		}
		textCountCurrent.GetComponent<RectTransform>().localPosition = new Vector3(textCountCurrentRecordPositionX,buttonLevel [tempFirstButtonLevelNumber].transform.localPosition.y-buttonInterval*(-0.6f),0);
		textCountMaximun.GetComponent<RectTransform>().localPosition = new Vector3(textCountMaximunRecordPositionX,buttonLevel [tempFirstButtonLevelNumber].transform.localPosition.y-buttonInterval*(-0.6f),0);
		textCountCurrent.GetComponent<EffectMoveInOut2D> ().RecordPosition ();
		textCountMaximun.GetComponent<EffectMoveInOut2D> ().RecordPosition ();
	}
}
