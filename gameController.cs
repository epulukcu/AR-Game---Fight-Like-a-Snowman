using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    public static gameController instance;
    public static bool allowMovement = false;
    public GameObject cameraButton; 
    public GameObject playerScoreOnScreen; 
    public GameObject enemyScoreOnScreen; 
    public GameObject backButton; 
    public GameObject fwdButton; 
    public GameObject attackButton; 
    private bool played321 = false; 
    public AudioClip[] audioClip;
    new AudioSource audio;
    public static int playerScore = 0;
    public static int enemyScore = 0;
    public GameObject[] points;
    public static int round = 0;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource> ();

    }
	
    private void playAudioTrack(int clip)
    {
        audio.clip = audioClip[clip];
        audio.Play();
    }

    public void scorePlayer()
    {
        playerScore++;
    }

    public void scoreEnemy()
    {
        enemyScore++;
    }

    public void doReset()
    {
        if (playerScore == 2)
        {
            playAudioTrack(6);
            StartCoroutine(showWinScreen());

        }
        else
        {
            playAudioTrack(5);
            StartCoroutine(showLoseScreen());

        }
    }
 
    public IEnumerator restartGame() //should be in the restart screen
    {
        yield return new WaitForSeconds(4.5f);
        points[0].SetActive(false);
        points[1].SetActive(false);
        points[2].SetActive(false);
        points[3].SetActive(false);
        allowMovement = true;
        StartCoroutine(restartRoundAudio());
    }

    public IEnumerator restartRoundAudio()
    {
        yield return new WaitForSeconds(2);
        playAudioTrack(0);
    }

    public IEnumerator showWinScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("youWon");
    }

    
    public IEnumerator showLoseScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("gameOver");
    }
    
	// Update is called once per frame
	void Update () {
        
        if (played321 == false)
        {
            //Buttons do not appear unless the camera is active
            if (DefaultTrackableEventHandler.trueFalse == true)
            {

                cameraButton.SetActive(false);
                playerScoreOnScreen.SetActive(true);
                enemyScoreOnScreen.SetActive(true);
                backButton.SetActive(true);
                fwdButton.SetActive(true);
                attackButton.SetActive(true); 
                played321 = true;
                StartCoroutine(round1());

           }
        } 
     

    }
    //The audios to be played before the game started is assigned in order
    IEnumerator round1 ()
    {
        yield return new WaitForSeconds(0);
        playAudioTrack(0); //Audio "Round 1"
        StartCoroutine(prepareYourself());
    }

    IEnumerator prepareYourself()
    {
        yield return new WaitForSeconds(1.8f);
        playAudioTrack(1);  //Audio "Prepare Yourself"
        StartCoroutine(start321());
    }

    IEnumerator start321()
    {
        yield return new WaitForSeconds(3f);
        playAudioTrack(2);  //Audio "3-2-1 Fight"
        StartCoroutine(allowplayerMovement());
    }
    //The movement of the characters was allowed after the audios ended.
    IEnumerator allowplayerMovement()
    {
        yield return new WaitForSeconds(5f); 
        allowMovement = true;
    }
    //Control of the points that appear when the characters win round.
    public void onScreenPoints()
    {
        if (playerScore == 1)
            points[0].SetActive(true);
        
        else if (playerScore == 2)
            points[1].SetActive(true);
        
        if (enemyScore == 1)
            points[2].SetActive(true);
 
        else if (enemyScore == 2)
            points[3].SetActive(true);
    }
    //Control of audios to be played between rounds
    public void rounds()
    {
        round = playerScore + enemyScore;
        if (round == 1)
            playAudioTrack(3); 

        if (round == 2 && playerScore!=2 && enemyScore!= 2)
            playAudioTrack(4); 
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        throw new System.NotImplementedException();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
