using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrashController : MonoBehaviour
{
    [SerializeField] GameObject m_crash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Crashed");
            //Debug.Log(other.contacts[0].point);
            //Debug.Log(other.gameObject.GetComponent<Rigidbody>().velocity);
            if (gameObject.transform.parent != null)
            {
                gameObject.transform.parent = null;
            }
            transform.DOMove(gameObject.transform.position + Vector3.forward * 1000f, 1f);
            //Instantiate(m_crash, other.contacts[0].point, m_crash.transform.rotation);
        }
    }
}
