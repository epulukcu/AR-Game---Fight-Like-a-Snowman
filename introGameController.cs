using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playGame() //switches the scene
    {
        SceneManager.LoadScene("UDT-Template");
    }

    public void Quit()
    {
        Debug.Log("has quit");
        Application.Quit();
    }
}
