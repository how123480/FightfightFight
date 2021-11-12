using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public int timer_i=0;
    private int invokeTime;

    public GameObject ItemAttackCandidate;
    public GameObject ItemSpeedCandidate;


	// Use this for initialization
    void timer()
    {
        timer_i++;
        Debug.Log(timer_i);
    }
	void Start () { 
        InvokeRepeating("timer",1f,1f);
        invokeTime =Random.Range(1,5);
	}
	
	// Update is called once per frame
	void Update () {
        if (timer_i == invokeTime)//生成道具
        {
            timer_i = 0;
            invokeTime = Random.Range(1, 5);
            int ItemNum = Random.Range(1,3);
            createItem(ItemNum);
        }
    }

    void createItem(int itemNum)
    {

        float x  = Random.Range(-8.48f,8.48f);
        float y = Random.Range(-8.48f,8.48f);

        if (itemNum == 1)//attack
        {
            GameObject itemAttackObj = GameObject.Instantiate(ItemAttackCandidate);
            ItemAttackScript itemAttackScript = itemAttackObj.GetComponent<ItemAttackScript>();
            itemAttackScript.Init();
            itemAttackScript.transform.position = new Vector3(x,y,0);
        }else if(itemNum == 2)//speed
        {
            GameObject itemSpeedObj = GameObject.Instantiate(ItemSpeedCandidate);
            ItemSpeedScript itemSpeedScript = itemSpeedObj.GetComponent<ItemSpeedScript>();
            itemSpeedScript.Init();
            itemSpeedScript.transform.position = new Vector3(x, y, 0);
        }
    }
}