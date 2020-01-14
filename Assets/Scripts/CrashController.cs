using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrashController : MonoBehaviour
{
    public GameObject m_crash;
    Rigidbody rbody;
    Vector3 m_force = new Vector3(0, 0, 100f);

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
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
            Debug.Log(other.contacts[0].point);
            //Debug.Log(other.gameObject.GetComponent<Rigidbody>().velocity);
            Instantiate(m_crash, other.contacts[0].point, m_crash.transform.rotation);
            rbody.AddForce(m_force);
            //transform.DOMove(gameObject.transform.position + Vector3.forward * 100f, 1f);
        }
    }
}
