using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void gameOver()
    {
        SceneManager.LoadScene("intro");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
