using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{
    private Vector3 vanishForce = new Vector3();
    Sequence sequence;

    //private float deadLine = -10; //消滅位置
    void Start()
    {
        vanishForce = new Vector3(0, 0, 200f);
        sequence = DOTween.Sequence();
    }


    // Update is called once per frame
    void Update()
    {
        /* //画面外にでたら破棄する
		if(transform.position.x < this.deadLine){
			Destroy(gameObject);
		}*/
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            sequence.Append(this.transform.DOMove(this.gameObject.transform.position + vanishForce, 1.5f));
            sequence.Join(this.transform.DOLocalRotate(new Vector3(0, 0, -1080f), 1.5f, RotateMode.FastBeyond360).SetRelative());
        }
    }
}
