using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemAttackScript : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;



    public void Init()
    {

        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
 
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
   

    }

    // Update is called once per frame
    void Update()
    {

    }
}
