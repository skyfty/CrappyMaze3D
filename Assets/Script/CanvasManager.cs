using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_MENU_PREPARE = 1;
	private const int STATE_MENU_IN = 2;
	private const int STATE_MENU_WAIT = 3;
	private const int STATE_MENU_OUT = 4;
	private const int STATE_MENU_LEVEL = 5;

	public float distance;
	public float speed;

	public GameObject canvasGame;
	public GameObject canvasLanguage;

	public GameObject gameManager;
	public GameObject buttonLevelManager;
	public GameObject textTitle;
	public GameObject buttonAbout;
	public GameObject buttonHelp;
	public GameObject buttonSound;
	public GameObject buttonExit;
	public GameObject imageJoystick;

	private Vector3 recordPositonImageTitle;
	private Vector3 recordPositionButtonAbout;
	private Vector3 recordPositionButtonHelp;
	private Vector3 recordPositionButtonSound;
//	private Vector3 recordPositionButtonSoundOff;
	private Vector3 recordPositionButtonExit;

	// Use this for initialization
	void Start () {
		state = STATE_WAIT;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			break;
		case STATE_MENU_PREPARE:
			textTitle.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			buttonAbout.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			buttonHelp.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			buttonSound.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
//			buttonSoundOff.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			buttonExit.GetComponent<EffectMoveInOut2D> ().PrepareMoveIn ();
			buttonLevelManager.GetComponent<ButtonLevelManager> ().PrepareMoveIn ();
			state = STATE_MENU_IN;
			break;
		case STATE_MENU_IN:
			textTitle.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			buttonAbout.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			buttonHelp.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			buttonSound.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			buttonExit.GetComponent<EffectMoveInOut2D> ().KeepMoveIn ();
			buttonLevelManager.GetComponent<ButtonLevelManager> ().KeepMoveIn ();
			if (textTitle.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () &&
				buttonAbout.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () &&
				buttonHelp.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () &&
				buttonSound.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () &&
				buttonExit.GetComponent<EffectMoveInOut2D> ().IsFinishMoveIn () &&
				buttonLevelManager.GetComponent<ButtonLevelManager> ().IsFinishMoveIn ()) {
				textTitle.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
				buttonAbout.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
				buttonHelp.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
				buttonSound.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
				buttonExit.GetComponent<EffectMoveInOut2D> ().PrepareMoveOut ();
				buttonLevelManager.GetComponent<ButtonLevelManager> ().PrepareMoveOut ();
				state = STATE_MENU_WAIT;

			}
			break;
		case STATE_MENU_WAIT:
			break;
		case STATE_MENU_OUT:
			textTitle.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			buttonAbout.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			buttonHelp.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			buttonSound.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			buttonExit.GetComponent<EffectMoveInOut2D> ().KeepMoveOut ();
			buttonLevelManager.GetComponent<ButtonLevelManager> ().KeepMoveOut ();
			if (textTitle.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () &&
				buttonAbout.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () &&
				buttonHelp.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () &&
				buttonSound.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () &&
				buttonExit.GetComponent<EffectMoveInOut2D> ().IsFinishMoveOut () &&
				buttonLevelManager.GetComponent<ButtonLevelManager> ().IsFinishMoveOut ()) {
				state = STATE_MENU_LEVEL;
			}
			break;
		case STATE_MENU_LEVEL:
			break;
		default:
			break;
		}
	}

	public void ResetWait (){
		if (state == STATE_MENU_PREPARE)
			state = STATE_WAIT;
		else
			print ("Wrong use CanvasManager.ResetWait()");
	}

	public void MoveIn () {
		if (state != STATE_WAIT && state != STATE_MENU_LEVEL)
			return;
		state = STATE_MENU_PREPARE;
	}

	public void MoveOut () {
		if (state != STATE_MENU_WAIT)
			return;
		state = STATE_MENU_OUT;
	}

	public bool IsFinishMoveIn () {
		if (state == STATE_MENU_WAIT)
			return true;
		return false;
	}

	public bool IsFinishMoveOut () {
		if (state == STATE_MENU_LEVEL)
			return true;
		return false;
	}
}