using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public GameObject gameManager;
	public GameObject canvasManager;
	public GameObject soundManager;
	public GameObject effectWin;

	public float touchDistance;
	public float speed;

	public Vector3 recordMousePosition;
	public Vector3 currentMousePosition;
	public bool isDrag;
	public bool isWin;

	private const float GRAVITY_VALUE = -0.098f;

	// Use this for initialization
	void Start () {
		PreparePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		//check gameManager state
		if (gameManager.GetComponent<GameManager>().IsStateLevel() == false)
			return;
		//keep gravity
		this.GetComponent<CharacterController>().Move(new Vector3(0,GRAVITY_VALUE,0));
		//Use Mouse
		if (Input.GetMouseButtonDown (0)) {
			recordMousePosition = Input.mousePosition;
			canvasManager.GetComponent<CanvasManager> ().imageJoystick.GetComponent<RectTransform> ().transform.localPosition = new Vector3 (
				canvasManager.GetComponent<RectTransform> ().rect.width * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).x - 0.5f),
				canvasManager.GetComponent<RectTransform> ().rect.height * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).y - 0.5f),
				0);
			canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (true);
			isDrag = true;
		}
		if (isDrag) {
			currentMousePosition = Input.mousePosition;
			if (Vector3.Distance (currentMousePosition, recordMousePosition) > touchDistance) {
				//this.transform.Translate (new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * speed * Time.deltaTime);
				this.GetComponent<CharacterController>().Move(new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * Time.deltaTime * speed);
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			isDrag = false;
			canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (false);
		}
		//Use Touch
		//		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
		//			recordTouchPosition = Input.GetTouch(0).position;
		//			recordFighterPosition = this.transform.position;
		//			isDrag = true;
		//		}
		//		if (isDrag) {
		//			tempFighterPositionX = recordFighterPosition.x + (Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position).x - Camera.main.ScreenToWorldPoint (recordTouchPosition).x);
		//			tempFighterPositionY = recordFighterPosition.y + (Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position).y - Camera.main.ScreenToWorldPoint (recordTouchPosition).y);
		//			if (tempFighterPositionX < MAX_LEFT_POSITION) tempFighterPositionX = MAX_LEFT_POSITION;
		//			if (tempFighterPositionX > MAX_RIGHT_POSITION) tempFighterPositionX = MAX_RIGHT_POSITION;
		//			if (tempFighterPositionY > MAX_TOP_POSITION) tempFighterPositionY = MAX_TOP_POSITION;
		//			if (tempFighterPositionY < MAX_BOTTOM_POSITION) tempFighterPositionY = MAX_BOTTOM_POSITION;
		//			this.transform.position = new Vector3 (tempFighterPositionX,tempFighterPositionY,this.transform.position.z);
		//			recordTouchPosition = Input.GetTouch(0).position;
		//			recordFighterPosition = this.transform.position;
		//		}
		//		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
		//			isDrag = false;
		//		}
	}

	private void OnControllerColliderHit(ControllerColliderHit setHit){
		if (setHit.gameObject.name == "Target") {
			if (gameManager.GetComponent<GameManager> ().IsStateLevel ()) {
				isWin = true;
				effectWin.GetComponent<ParticleSystem> ().Play ();
				soundManager.GetComponent<SoundManager> ().PlayWin ();
				canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (false);
			}
		}
	}

	public void PreparePlayer () {
		isDrag = false;
		isWin = false;
		effectWin.GetComponent<ParticleSystem> ().Stop ();
	}
}
