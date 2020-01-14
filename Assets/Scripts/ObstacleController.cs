using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{
    
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        /* //画面外にでたら破棄する
		if(transform.position.x < this.deadLine){
			Destroy(gameObject);
		}*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
