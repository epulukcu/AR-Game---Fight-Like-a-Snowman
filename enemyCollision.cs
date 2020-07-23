using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour {

    //Characters are provided not to collide with each other and stay at eye contact.
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyController.instance.enemyReact (); 
            Debug.Log("HIT");
        }
    }
}
