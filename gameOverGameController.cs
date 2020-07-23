using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame() //switches the scene
    {
        SceneManager.LoadScene("UDT-Template");

        fighterController.instance.playerHB.value = 100; //to start a fresh round 
        fighterController.instance.health = 100;

        enemyController.instance.enemyHB.value = 100;
        enemyController.instance.enemyHealth = 100;
        gameController.playerScore = 0;
        gameController.enemyScore = 0;
        StartCoroutine(gameController.instance.restartGame());

    }

    public void Quit()
    {
        Debug.Log("has quit");
        Application.Quit();
    }
}
