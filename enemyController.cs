using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class enemyController : MonoBehaviour {

    public Transform playerTransform;
    private Vector3 direction;
    static Animator anim2;
    public int enemyHealth = 100; 
    public static enemyController instance; 
    public Slider enemyHB;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audio;
    private Vector3 enemyPosition;

    // Awake func.
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
       anim2 = GetComponent<Animator>();
       SetAllBoxColliders(false);
       audio = GetComponent<AudioSource>();
       enemyPosition = transform.position;
    
	}
    //Character-specific sounds kept in an array
    public void playAudio(int clip)
    {
        audio.clip = audioClip[clip];
        audio.Play();
    }
    //The array where "box collider" are kept to control collisions.
    private void SetAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim2.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {

            //for enemy to keep looking at the player
            direction = playerTransform.position - this.transform.position;
            direction.y = 0; //to prevent characters leaning when they get close
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            SetAllBoxColliders(false);

        }

        Debug.Log(direction.magnitude);
        //Control of what the character managed by computer will do in what situations.
        if (direction.magnitude > 12f && gameController.allowMovement == true) //if the other character is too far, get closer
        {
            anim2.SetTrigger("walk"); 
            transform.position += 10 * (transform.forward * Time.deltaTime); 
            audio.Stop();
            SetAllBoxColliders(false);
            
        }
        else
            anim2.ResetTrigger("walk"); 
        
       
        if (direction.magnitude < 5f && gameController.allowMovement == true)
        {
        
            SetAllBoxColliders(true);  //attack if it is at the specified distance
            if (!audio.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("atk01")) {
                playAudio(0);
                anim2.SetTrigger("atk01"); 
            }
                
        }
        else
        {
            anim2.ResetTrigger("atk01");
        }
     
        if (direction.magnitude > 0f && direction.magnitude < 2.25f && gameController.allowMovement == true)
        {
            anim2.SetTrigger("walk"); //get away if they are too close
            transform.position -= 10 * (transform.forward * Time.deltaTime);
            SetAllBoxColliders(false);
            audio.Stop();
        }
        else
            anim2.ResetTrigger("walk");

    }

    //Character accrues health impact is reduced and convenient audio player.
    public void enemyReact()
    {
        enemyHealth = enemyHealth - 10; 
        //character knocked-out
        if (enemyHealth < 10) {
            enemyKnockout();
            playAudio(2);
        }
            

        else
        {
            anim2.ResetTrigger ("idle");
            anim2.SetTrigger ("react");
            playAudio(1);  
        }
        enemyHB.value = enemyHealth;
    }

    public void enemyKnockout()
    {
        gameController.allowMovement = false;
        
        anim2.SetTrigger("knockout");
        gameController.instance.scorePlayer();
        gameController.instance.onScreenPoints();
        gameController.instance.rounds();
        

        if (gameController.playerScore == 2)
        {
            gameController.instance.doReset();
            StartCoroutine(resetCharacters()); 
         
        } else
        {
            StartCoroutine(resetCharacters());
            
        }

    }
    //Resetting all the features of character.
    IEnumerator resetCharacters ()
    {
        enemyHealth = 100; 
        enemyHB.value = 100;
        fighterController.instance.health = 100;
        fighterController.instance.playerHB.value = 100;
        yield return new WaitForSeconds(4);
        anim2.SetTrigger("idle"); 
        anim2.ResetTrigger("knockout"); 
        gameController.allowMovement = true;
    }
}
