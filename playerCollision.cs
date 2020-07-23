using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
{
    //Characters are provided not to collide with each other and stay at eye contact.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            fighterController.instance.stun();
            Debug.Log("Enemy HIT");
        }
    }
}