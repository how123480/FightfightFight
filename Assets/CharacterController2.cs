using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController2 : MonoBehaviour {

	private Rigidbody2D rigidbody;
	private Animator animator;
	private Vector3 direction;
	public GameObject attack;
	int action =0;
	public float speed = 5.0f;
	
	public float Mp,MaxMp;
	public int attacklevel;
	private const float maxSpeed = 6.5f;
    private const int maxAttackLevel = 7;

	public int ultilevel = 9;	
	public ParticleSystem mp_Full_Effect;

	public bool dead;

	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
		attacklevel=1;
		direction.x=0;
		direction.y=-1;
		dead=false;
		Mp=0.0f;
		MaxMp=100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Mp>=MaxMp){
			mp_Full_Effect.transform.position = this.transform.position;
			mp_Full_Effect.gameObject.SetActive (true);
		}else{
			mp_Full_Effect.gameObject.SetActive(false);
		}
		if(!dead){
			//普攻
			if(Input.GetKeyDown(KeyCode.C)){
				Vector3 rdirection=direction;
				Vector3 ldirection=direction;

				rdirection.x=direction.y;
				rdirection.y=direction.x;
				ldirection=rdirection * -1;
		
				//rightattack
				GameObject st_attackobjr = GameObject.Instantiate(attack);
				st_attackobjr.tag="c2";
				attackScript st_attackscriptr=st_attackobjr.GetComponent<attackScript>();
				st_attackscriptr.transform.position = this.transform.position + rdirection;
				st_attackscriptr.Init();
				this.myInvoke(0.2f,()=>{
					Destroy(st_attackobjr);
				});
				//leftattack
				GameObject st_attackobjl = GameObject.Instantiate(attack);
				st_attackobjl.tag="c2";
				attackScript st_attackscriptl=st_attackobjl.GetComponent<attackScript>();
				st_attackscriptl.transform.position = this.transform.position + ldirection;
				st_attackscriptl.Init();
				this.myInvoke(0.2f,()=>{
					Destroy(st_attackobjl);
				});
				Attack(attacklevel,this.transform.position,direction);
			}
			//續力	 
			if(Input.GetKey(KeyCode.V)){
					if(Mp<MaxMp)
						Mp+=0.2f;
					Debug.Log(Mp);
			}

			//大絕
			if(Input.GetKey(KeyCode.B)){
				if(Mp>=MaxMp){
					ultimate(ultilevel,this.transform.position);
					Mp=0.0f;
				}
			}
			if(Input.GetKey(KeyCode.W)){
				rigidbody.velocity =  new Vector2(0,speed);
				animator.SetInteger("action",1);
				direction.x=0;
				direction.y=1;
			}
			else if(Input.GetKey(KeyCode.S)){
				rigidbody.velocity =  new Vector2(0,-speed);
				animator.SetInteger("action",2);
				direction.x=0;
				direction.y=-1;
			}
			else if(Input.GetKey(KeyCode.D)){
				rigidbody.velocity =  new Vector2(speed,0);
				animator.SetInteger("action",3);
				direction.x=1;
				direction.y=0;
			}
			else if(Input.GetKey(KeyCode.A)){
				rigidbody.velocity =  new Vector2(-speed,0);
				animator.SetInteger("action",4);
				direction.x=-1;
				direction.y=0;
			}
			else {
				rigidbody.velocity =  new Vector2(0,0);
				animator.SetInteger("action",0);
			}
		}else {
			Debug.Log ("DEAD!!!!!");
			rigidbody.velocity =  new Vector2(0,0);
			animator.SetInteger("action",5);
			Application.LoadLevel ("gameoverscene2");
		}
	}
	private void Attack(int attacklevell,Vector3 position, Vector3 d){
		
		if(attacklevell>0){
			Vector3 rdirection=d;
			Vector3 ldirection=d;

			rdirection.x=d.y;
			rdirection.y=d.x;
			ldirection=rdirection * -1;

			//for(int i=1;i<=attacklevel;++i){
			GameObject attackobj = GameObject.Instantiate(attack);
			attackobj.tag="c2";
			attackScript attackscript=attackobj.GetComponent<attackScript>();
			attackscript.transform.position = position + (attacklevel-attacklevell+1)*d;
			attackscript.Init();
			this.myInvoke(0.2f,()=>{
				Destroy(attackobj);
			});
			//rightattack
			GameObject attackobjr = GameObject.Instantiate(attack);
			attackobjr.tag="c2";
			attackScript attackscriptr=attackobjr.GetComponent<attackScript>();
			attackscriptr.transform.position = position + rdirection +(attacklevel-attacklevell+1)*d;
			attackscriptr.Init();
			this.myInvoke(0.2f,()=>{
				Destroy(attackobjr);
			});
			//leftattack
			GameObject attackobjl = GameObject.Instantiate(attack);
			attackobjl.tag="c2";
			attackScript attackscriptl=attackobjl.GetComponent<attackScript>();
			attackscriptl.transform.position = position + ldirection + (attacklevel-attacklevell+1)*d;
			attackscriptl.Init();
			this.myInvoke(0.2f,()=>{
				Destroy(attackobjl);
			});
			this.myInvoke(0.01f,()=>{
				Attack(attacklevell-1,position,d);
			});
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "c1" && col.gameObject.name == "attack(Clone)"){
			//this.gameObject.SetActive(false);
			dead=true;
			animator.SetInteger("action",5);

		}else if (col.tag == "Item_Attack"){
            Destroy(col.gameObject);
            if (this.attacklevel <= maxAttackLevel){
                this.attacklevel ++;
				this.myInvoke(5f,()=>{
					this.attacklevel--;
				});
			}
        }else if (col.tag == "Item_Speed"){
            Destroy(col.gameObject);
            if(this.speed <= maxSpeed)
                this.speed +=0.5f;
        }
		Debug.Log(col.gameObject.name);

	}
	

	private void ultimate(int ultilevell,Vector3 position){
		Vector3 unit_vec_y=new Vector3(0,1,0);
		Vector3 unit_vec_x=new Vector3(1,0,0);

		if(ultilevell>0 ){
			for(int i =-(ultilevel-ultilevell+1);i<=(ultilevel-ultilevell+1);++i){
				GameObject attackobj = GameObject.Instantiate(attack);
				attackobj.tag = "c2";
				attackScript attackscript = attackobj.GetComponent<attackScript>();
				attackscript.transform.position = position + unit_vec_x*(ultilevel-ultilevell+1) + unit_vec_y*i;
				attackscript.Init();
				this.myInvoke(0.2f, () =>
           		 {
                	Destroy(attackobj);
            	});
			}
			for(int i=-(ultilevel-ultilevell+1);i<=(ultilevel-ultilevell+1);++i){
				GameObject attackobj = GameObject.Instantiate(attack);
				attackobj.tag = "c2";
				attackScript attackscript = attackobj.GetComponent<attackScript>();
				attackscript.transform.position = position + unit_vec_y*(ultilevel-ultilevell+1) + unit_vec_x*i;
				attackscript.Init();
				this.myInvoke(0.2f, () =>
           		 {
                	Destroy(attackobj);
            	});
			}
			for(int i =-(ultilevel-ultilevell+1);i<=(ultilevel-ultilevell+1);++i){
				GameObject attackobj = GameObject.Instantiate(attack);
				attackobj.tag = "c2";
				attackScript attackscript = attackobj.GetComponent<attackScript>();
				attackscript.transform.position = position - unit_vec_x*(ultilevel-ultilevell+1) + unit_vec_y*i;
				attackscript.Init();
				this.myInvoke(0.2f, () =>
           		 {
                	Destroy(attackobj);
            	});
			}
			for(int i=-(ultilevel-ultilevell+1);i<=(ultilevel-ultilevell+1);++i){
				GameObject attackobj = GameObject.Instantiate(attack);
				attackobj.tag = "c2";
				attackScript attackscript = attackobj.GetComponent<attackScript>();
				attackscript.transform.position = position - unit_vec_y*(ultilevel-ultilevell+1) + unit_vec_x*i;
				attackscript.Init();
				this.myInvoke(0.2f, () =>
           		 {
                	Destroy(attackobj);
            	});
			}
			this.myInvoke(0.3f,()=>{
				ultimate(ultilevell-1,position);
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