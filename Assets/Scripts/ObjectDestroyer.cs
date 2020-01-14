using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.z > this.transform.position.z + 3000f)
        {
            Destroy(this.gameObject);
        }
    }
}
