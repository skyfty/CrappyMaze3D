using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public bool isUse;

	public GameObject pillarFrontLeft;
	public GameObject pillarFrontRight;
	public GameObject pillarBehindLeft;
	public GameObject pillarBehindRight;
	public GameObject wallFront;
	public GameObject wallBehind;
	public GameObject wallLeft;
	public GameObject wallRight;

	public bool hasFront;
	public bool hasBehind;
	public bool hasLeft;
	public bool hasRight;

	public int deepToPlayer;
	public float delay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
