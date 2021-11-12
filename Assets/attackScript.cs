using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour {

	// Use this for initialization

	private Rigidbody2D rigidbody;
	private SpriteRenderer spriterender;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(){
		rigidbody=this.GetComponent<Rigidbody2D>();
		spriterender=this.GetComponent<SpriteRenderer>();
		
	}
}
