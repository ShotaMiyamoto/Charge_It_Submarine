using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position + new Vector3(0, 0, 5f);
    }
}
