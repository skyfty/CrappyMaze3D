using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	public GameObject unitPrefab;

	public int rowCount;
	public int columnCount;

	public int playerRowNumber;
	public int playerColumnNumber;
	public int targetRowNumber;
	public int targetColumnNumber;

	public GameObject[,] unit;

	private const int DIRECTION_FRONT = 0;
	private const int DIRECTION_BEHIND = 1;
	private const int DIRECTION_LEFT = 2;
	private const int DIRECTION_RIGHT = 3;
	private const int DIRECTION_NONE = -1;

	public float unitPositionY;
	public float unitMaxiumnDelay;
	public float unitSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuildMaze (int setRowCount, int setColumnCount) {
		//destroy all the old units
		for (int i = 0; i < this.transform.childCount; i++) {
			Destroy (this.transform.GetChild (i).gameObject);
		}
		//create new units
		rowCount = setRowCount;
		columnCount = setColumnCount;

		unit = new GameObject[rowCount,columnCount];

		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				unit [i, j] = Instantiate (unitPrefab) as GameObject;
				unit [i, j].transform.parent = this.transform;
				unit [i, j].transform.position = new Vector3 (j,unitPositionY,i);		//new Vector3 (j,unit [i, j].transform.position.y,i);
				unit [i, j].GetComponent<Unit>().isUse = false;
				unit [i, j].GetComponent<Unit> ().deepToPlayer = 0;
				unit [i, j].GetComponent<Unit> ().hasFront = true;
				unit [i, j].GetComponent<Unit> ().hasBehind = true;
				unit [i, j].GetComponent<Unit> ().hasLeft = true;
				unit [i, j].GetComponent<Unit> ().hasRight= true;
				unit [i, j].GetComponent<Unit> ().delay = Random.Range(0.0f, unitMaxiumnDelay);
			}
		}

		playerRowNumber = Random.Range (0, rowCount);
		playerColumnNumber = Random.Range (0, columnCount);
		print ("playerRowNumber"+playerRowNumber+"  playerColumnNumber   "+playerColumnNumber);
		unit [playerRowNumber, playerColumnNumber].GetComponent<Unit> ().isUse = true;

		//Destroy (unit [playerRowNumber, playerColumnNumber]);

		RecursionUnit (playerRowNumber,playerColumnNumber,DIRECTION_NONE,0);

		targetRowNumber = 0;
		targetColumnNumber = 0;
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				if (unit [i, j].GetComponent<Unit> ().deepToPlayer > unit [targetRowNumber, targetColumnNumber].GetComponent<Unit> ().deepToPlayer) {
					targetRowNumber = i;
					targetColumnNumber = j;
				}
			}
		}
	}

	//di gui function
	public void RecursionUnit(int setRowNumber, int setColumnNumber, int setFromDirection, int setDeepToPlayer){
		//if wrong number will stop
		if (setRowNumber < 0 || setRowNumber >= rowCount || setColumnNumber < 0 || setColumnNumber >= columnCount){
			return;
		}
		//clear fromDirection way
		switch (setFromDirection) {
		case DIRECTION_FRONT:
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallBehind.SetActive (false);
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasBehind = false;
			break;
		case DIRECTION_BEHIND:
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallFront.SetActive (false);
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasFront = false;
			break;
		case DIRECTION_LEFT:
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallRight.SetActive (false);
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasRight = false;
			break;
		case DIRECTION_RIGHT:
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallLeft.SetActive (false);
			unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasLeft = false;
			break;
		case DIRECTION_NONE:
			break;
		default:
			break;
		}
		//set base param
		unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().isUse = true;
		unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().deepToPlayer = setDeepToPlayer;
		int nextRowNumber = setRowNumber;
		int nextColumnNumber = setColumnNumber;
		//get random direction
		int[] nextDirection = new int [4];
		for (int i = 0; i < 4; i++) {
			nextDirection [i] = -1;
		}
		int tempDirection;
		for (int i = 0; i < 4; i++) {
			do {
				tempDirection = Random.Range (0, 4);
			} while(nextDirection [tempDirection] != -1);
			nextDirection [tempDirection] = i;
		}
		//clear every direction
		for (int i = 0; i < 4; i++) {
			nextRowNumber = setRowNumber;
			nextColumnNumber = setColumnNumber;
			switch(nextDirection [i]){
			case DIRECTION_FRONT:
				//print ("DIRECTION_FRONT");
				nextRowNumber = setRowNumber + 1;
				if (nextRowNumber >= rowCount) {
					break;
				}
				if (unit [nextRowNumber, nextColumnNumber].GetComponent<Unit> ().isUse == true) {
					break;
				}
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallFront.SetActive (false);
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasFront = false;
				RecursionUnit (nextRowNumber, nextColumnNumber, DIRECTION_FRONT, setDeepToPlayer + 1);
				break;
			case DIRECTION_BEHIND:
				//print("DIRECTION_BEHIND");
				nextRowNumber = setRowNumber - 1;
				if (nextRowNumber < 0) {
					break;
				}
				if (unit [nextRowNumber, nextColumnNumber].GetComponent<Unit> ().isUse == true) {
					break;
				}
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallBehind.SetActive (false);
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasBehind = false;
				RecursionUnit (nextRowNumber, nextColumnNumber, DIRECTION_BEHIND, setDeepToPlayer + 1);
				break;
			case DIRECTION_LEFT:
				//print("DIRECTION_LEFT");
				nextColumnNumber = setColumnNumber - 1;
				if (nextColumnNumber < 0) {
					break;
				}
				if (unit [nextRowNumber, nextColumnNumber].GetComponent<Unit> ().isUse == true) {
					break;
				}
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallLeft.SetActive (false);
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasLeft = false;
				RecursionUnit (nextRowNumber, nextColumnNumber, DIRECTION_LEFT, setDeepToPlayer + 1);
				break;
			case DIRECTION_RIGHT:
				//print("DIRECTION_RIGHT");
				nextColumnNumber = setColumnNumber + 1;
				if (nextColumnNumber >= columnCount) {
					break;
				}
				if (unit [nextRowNumber, nextColumnNumber].GetComponent<Unit> ().isUse == true) {
					break;
				}
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().wallRight.SetActive (false);
				unit [setRowNumber, setColumnNumber].GetComponent<Unit> ().hasRight = false;
				RecursionUnit (nextRowNumber, nextColumnNumber, DIRECTION_RIGHT, setDeepToPlayer + 1);
				break;
			default:
				break;
			}
		}
	}

	public void PrepareMoveIn () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				unit [i, j].transform.position = new Vector3 (j,unitPositionY,i);		//new Vector3 (j,unit [i, j].transform.position.y,i);
				unit [i, j].GetComponent<Unit> ().delay = Random.Range(0.0f, unitMaxiumnDelay);
			}
		}
	}

	public void KeepMoveIn () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				unit [i, j].GetComponent<Unit> ().delay -= Time.deltaTime;
				if (unit [i, j].GetComponent<Unit> ().delay < 0.0f) {
					unit [i, j].transform.position = new Vector3 (j,unit [i, j].transform.position.y - unitSpeed,i);
					if (unit [i, j].transform.position.y < 0.0f) {
						unit [i, j].transform.position = new Vector3 (j,0.0f,i);
					}
				}
			}
		}
	}

	public bool IsFinishMoveIn () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				if (unit [i, j].transform.position.y != 0.0f)
					return false;
			}
		}
		return true;
	}

	public void PrepareMoveOut () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				unit [i, j].transform.position = new Vector3 (j,0.0f,i);		//new Vector3 (j,unit [i, j].transform.position.y,i);
				unit [i, j].GetComponent<Unit> ().delay = Random.Range(0.0f, unitMaxiumnDelay);
			}
		}
	}

	public void KeepMoveOut () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				unit [i, j].GetComponent<Unit> ().delay -= Time.deltaTime;
				if (unit [i, j].GetComponent<Unit> ().delay < 0.0f) {
					unit [i, j].transform.position = new Vector3 (j,unit [i, j].transform.position.y + unitSpeed,i);
					if (unit [i, j].transform.position.y > unitPositionY) {
						unit [i, j].transform.position = new Vector3 (j,unitPositionY,i);
					}
				}
			}
		}
	}

	public bool IsFinishMoveOut () {
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < columnCount; j++) {
				if (unit [i, j].transform.position.y != unitPositionY)
					return false;
			}
		}
		return true;
	}
}
