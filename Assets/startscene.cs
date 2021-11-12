using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class startscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			Debug.Log ("A key or mouse click has been detected");
			Application.LoadLevel (1);
		}
	}
}
