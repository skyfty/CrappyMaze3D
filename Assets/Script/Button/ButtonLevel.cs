using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour {

	public int number;
	public int rowCount;
	public int columnCount;
	public GameObject gameManager;
	public GameObject buttonLevelManager;
	public GameObject mark;
	public GameObject lockset;

	private int state;
	private const int STATE_LOCK = 0;
	private const int STATE_CURRENT = 1;
	private const int STATE_FINISH = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		if (number > buttonLevelManager.GetComponent<ButtonLevelManager> ().finishLevel + 1)
			return;
		gameManager.GetComponent<GameManager> ().PlayLevel (number, rowCount, columnCount);

	}

	public void SetLock () {
		mark.transform.gameObject.SetActive (false);
		lockset.transform.gameObject.SetActive (true);
	}

	public void SetCurrent () {
		mark.transform.gameObject.SetActive (false);
		lockset.transform.gameObject.SetActive (false);
	}

	public void SetFinish () {
		mark.transform.gameObject.SetActive (true);
		lockset.transform.gameObject.SetActive (false);
	}
}
