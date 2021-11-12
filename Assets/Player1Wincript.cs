using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Wincript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log ("A key or mouse click has been detected");
			Application.LoadLevel ("start");
		}
	}
}
