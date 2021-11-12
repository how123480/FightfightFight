using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedScript : MonoBehaviour {

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    public void Init () {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
