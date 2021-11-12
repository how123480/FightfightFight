using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Extensions{
//System.Action is any "function"
  public static void myInvoke(this MonoBehaviour mb, float seconds,System.Action completeEvent)
  {
      mb.StartCoroutine(waitSeconds(seconds,completeEvent));
  }
  public static IEnumerator waitSeconds(float seconds,System.Action completeEvent)
  {
      yield return new WaitForSeconds(seconds);
      completeEvent();
  }
}

public class characterController : MonoBehaviour {
	private Rigidbody2D rigidbody;
	private Animator animator;
	private Vector3 direction;

	public GameObject attack;

	int action =0;
	public float speed = 5.0f;
	
	public float Mp,MaxMp;
	public int attacklevel;
	public bool dead;
	private const float maxSpeed = 6.5f;
    private const int maxAttackLevel = 9;
	public ParticleSystem mp_Full_Effect;
	private const int ultimateLevel=20;

	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
		direction.x=0;
		direction.y=-1;
		attacklevel=5;
		dead=false;
		Mp=0.0f;
		MaxMp=100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//普攻
		if(Mp>=MaxMp){
			mp_Full_Effect.transform.position = this.transform.position;
			mp_Full_Effect.gameObject.SetActive (true);
		}else{
			mp_Full_Effect.gameObject.SetActive (false);
		}

		if(!dead){
			if(Input.GetKeyDown(KeyCode.RightShift)){
				Attack(attacklevel,this.transform.position,direction);
			}
			//大絕
			if(Input.GetKey(KeyCode.RightAlt)){
				if(Mp>=MaxMp){
					ultimate(this.transform.position,ultimateLevel);
					Mp=0.0f;
				}
			}
			if(Input.GetKey(KeyCode.Return)){
				if(Mp<MaxMp)
					Mp+=0.2f;
				Debug.Log(Mp);
			}

			if(Input.GetKey(KeyCode.UpArrow)){
				rigidbody.velocity =  new Vector2(0,speed);
				animator.SetInteger("action",1);
				direction.x=0;
				direction.y=1;
			}
			else if(Input.GetKey(KeyCode.DownArrow)){
				rigidbody.velocity =  new Vector2(0,-speed);
				animator.SetInteger("action",2);
				direction.x=0;
				direction.y=-1;
			}
			else if(Input.GetKey(KeyCode.RightArrow)){
				rigidbody.velocity =  new Vector2(speed,0);
				animator.SetInteger("action",3);
				direction.x=1;
				direction.y=0;
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				rigidbody.velocity =  new Vector2(-speed,0);
				animator.SetInteger("action",4);
				direction.x=-1;
				direction.y=0;
			}else{
				rigidbody.velocity =  new Vector2(0,0);
					animator.SetInteger("action",0);
			}
		}else {
				rigidbody.velocity =  new Vector2(0,0);
				animator.SetInteger("action",5);
				Application.LoadLevel ("gameoverscene");
		}
		
	}
	
	private void Attack(int attacklevel1,Vector3 position,Vector3 d){

        //for(int i=1;i<=attacklevel;++i){
        if (attacklevel1 > 0)
        {
            GameObject attackobj = GameObject.Instantiate(attack);
            attackobj.tag = "c1";
            attackScript attackscript = attackobj.GetComponent<attackScript>();
            attackscript.transform.position = position + (attacklevel-attacklevel1) * d;
            attackscript.Init();
            this.myInvoke(0.2f, () =>
            {
                Destroy(attackobj);
            });
            this.myInvoke(0.05f, () =>
            {
                Attack(attacklevel1-1,position,d);
            });
        }
        //}

    }
	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "c2" && col.gameObject.name == "attack(Clone)"){
			//this.gameObject.SetActive(false);
			dead=true;
			animator.SetInteger("action",5);
		}else if (col.tag == "Item_Attack"){
            Destroy(col.gameObject);
            if (this.attacklevel <= maxAttackLevel){
                this.attacklevel +=2;
				this.myInvoke(5f,()=>{
					this.attacklevel-=2;
				});
			}
        }
        else if (col.tag == "Item_Speed"){
            Destroy(col.gameObject);
            if(this.speed <= maxSpeed)
                this.speed +=0.5f;
        }
		 Debug.Log(col.gameObject.name);
	}
	
	private void ultimate(Vector3 position,int ultimateLevel1)
    {
        if (ultimateLevel1 > 0)
        {
            Vector3 x = new Vector3(1, 0, 0);
            Vector3 _x = new Vector3(-1, 0, 0);
            Vector3 y = new Vector3(0, 1, 0);
            Vector3 _y = new Vector3(0, -1, 0);
            int block = ultimateLevel - ultimateLevel1;
            for(int i = 1; i <= 8; i++)
            {
                GameObject attackobj = GameObject.Instantiate(attack);
                attackobj.tag = "c1";
                attackScript attackscript = attackobj.GetComponent<attackScript>();
                attackscript.Init();
                if      (i == 1) attackscript.transform.position = position + block * x;
                else if (i == 2) attackscript.transform.position = position + block * x + block * y;
                else if (i == 3) attackscript.transform.position = position + block * y;
                else if (i == 4) attackscript.transform.position = position + block *_x + block * y;
                else if (i == 5) attackscript.transform.position = position + block *_x;
                else if (i == 6) attackscript.transform.position = position + block *_x + block *_y;
                else if (i == 7) attackscript.transform.position = position + block *_y;
                else if (i == 8) attackscript.transform.position = position + block * x + block *_y;
                this.myInvoke(0.2f, () =>
                {
                    Destroy(attackobj);
                    
                });
            }
            this.myInvoke(0.1f, () =>
            {
                ultimate(position, ultimateLevel1 - 1);
            });
        }
	}
	/*
	void Awake() {//for changing scene
		DontDestroyOnLoad (transform.gameObject);
	}

	void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) {
		if (scene.name == "start") {
			Destroy(gameObject);
			Debug.Log("I am inside the if statement");
		}
	}
*/
	public bool getIsDead(){
		return dead;
	}
		
	
}
