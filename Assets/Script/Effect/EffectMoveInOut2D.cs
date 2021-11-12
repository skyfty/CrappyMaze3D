using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMoveInOut2D : MonoBehaviour {

	private Vector3 recordPositionFrom;
	private Vector3 recordPositionTo;
	public Direction2D directionIn;
	public float distance;
	public float speed;
	public enum Direction2D
	{
		Direction2DNone = 0,
		Direction2DUp = 1,
		Direction2DDown = 2,
		Direction2DLeft = 3,
		Direction2DRight = 4
	}

	// Use this for initialization
	void Start () {
		RecordPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RecordPosition (){
		recordPositionTo = this.transform.localPosition;
		switch (directionIn) {
		case Direction2D.Direction2DUp:
			recordPositionFrom = new Vector3 (recordPositionTo.x, recordPositionTo.y + distance, recordPositionTo.z);
			break;
		case Direction2D.Direction2DDown:
			recordPositionFrom = new Vector3 (recordPositionTo.x, recordPositionTo.y - distance, recordPositionTo.z);
			break;
		case Direction2D.Direction2DLeft:
			recordPositionFrom = new Vector3 (recordPositionTo.x - distance, recordPositionTo.y, recordPositionTo.z);
			break;
		case Direction2D.Direction2DRight:
			recordPositionFrom = new Vector3 (recordPositionTo.x + distance, recordPositionTo.y, recordPositionTo.z);
			break;
		case Direction2D.Direction2DNone:
			recordPositionFrom = recordPositionTo;
			break;
		default:
			break;
		}
	}

	public void KeepMoveIn () {
		switch (directionIn) {
		case Direction2D.Direction2DUp:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y - speed, this.transform.localPosition.z);
			if (this.transform.localPosition.y < recordPositionTo.y)
				this.transform.localPosition = recordPositionTo;
			break;
		case Direction2D.Direction2DDown:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y + speed, this.transform.localPosition.z);
			if (this.transform.localPosition.y > recordPositionTo.y)
				this.transform.localPosition = recordPositionTo;
			break;
		case Direction2D.Direction2DLeft:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);
			if (this.transform.localPosition.x > recordPositionTo.x)
				this.transform.localPosition = recordPositionTo;
			break;
		case Direction2D.Direction2DRight:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - speed, this.transform.localPosition.y, this.transform.localPosition.z);
			if (this.transform.localPosition.x < recordPositionTo.x)
				this.transform.localPosition = recordPositionTo;
			break;
		case Direction2D.Direction2DNone:
			recordPositionFrom = recordPositionTo;
			break;
		default:
			break;
		}

	}

	public void KeepMoveOut () {
		switch (directionIn) {
		case Direction2D.Direction2DUp:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y + speed, this.transform.localPosition.z);
			if (this.transform.localPosition.y > recordPositionFrom.y)
				this.transform.localPosition = recordPositionFrom;
			break;
		case Direction2D.Direction2DDown:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y - speed, this.transform.localPosition.z);
			if (this.transform.localPosition.y < recordPositionFrom.y)
				this.transform.localPosition = recordPositionFrom;
			break;
		case Direction2D.Direction2DLeft:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - speed, this.transform.localPosition.y, this.transform.localPosition.z);
			if (this.transform.localPosition.x < recordPositionFrom.x)
				this.transform.localPosition = recordPositionFrom;
			break;
		case Direction2D.Direction2DRight:
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);
			if (this.transform.localPosition.x > recordPositionFrom.x)
				this.transform.localPosition = recordPositionFrom;
			break;
		case Direction2D.Direction2DNone:
			this.transform.localPosition = recordPositionFrom;
			break;
		default:
			break;
		}

	}

	public void PrepareMoveIn () {
		this.transform.localPosition = recordPositionFrom;
	}

	public void PrepareMoveOut () {
		this.transform.localPosition = recordPositionTo;
	}

	public bool IsFinishMoveIn () {
		if (this.transform.localPosition == recordPositionTo) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsFinishMoveOut () {
		if (this.transform.localPosition == recordPositionFrom) {
			return true;
		} else {
			return false;
		}
	}
}
