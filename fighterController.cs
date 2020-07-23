using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class fighterController : MonoBehaviour
{
    public Transform enemyTarget;
    static Animator anim;
    public static bool mvBack = false;
    public static bool mvFWD = false;
    public static fighterController instance;
    public static bool isAttacking = false;
    private Vector3 direction;
    public int health = 100; 
    public Slider playerHB; 
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audio;
    private Vector3 playerPosition;


    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        SetAllBoxColliders(false);
        audio = GetComponent<AudioSource>();
        playerPosition = transform.position;
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            //for player to keep looking at the enemy
            direction = enemyTarget.position - this.transform.position;
            direction.y = 0; //to prevent leaning when characters get close
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")) { //if the fighter is in idle mode
            isAttacking = false;
            SetAllBoxColliders(false);
        }
        if (gameController.allowMovement == true)
        {

            //and if its not fighting, it can move
            if (isAttacking == false)
            {
                if (mvBack == true)
                {
                    anim.SetTrigger("walk"); 
                    transform.position -= 10 * (transform.forward * Time.deltaTime);
                    SetAllBoxColliders(false);
                }

                else
                {
                    anim.SetTrigger("idle");
                    anim.ResetTrigger("walk"); 
                }

                if (mvFWD == true)
                { //moving forward 
                    anim.SetTrigger("walk"); 
                    transform.position += 10 * (transform.forward * Time.deltaTime);
                    SetAllBoxColliders(false);
                }

                else if (mvBack == false)
                {
                    anim.SetTrigger("idle");
                    anim.ResetTrigger("walk");
                }
            }
            else if (isAttacking == true)
            {
                SetAllBoxColliders(true);
            }
        }

    }

    public void atk01() //attacking
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("atk01");
        playAudio(0); // attack sound
    }

    public void stun() //getting hit by the enemy
    {
        isAttacking = true;
        health = health - 10;
        if(health < 10)
        {
            knockout();
            playAudio(1); // game over sound / knockout sound
        }
        else
        {
            anim.ResetTrigger("idle");
            anim.SetTrigger("stun");
            playAudio(2); //react sound 
        }
        playerHB.value = health;
    }

    public void knockout()
    {
        gameController.allowMovement = false;
           
        anim.SetTrigger("knockout");
        gameController.instance.scoreEnemy();
        gameController.instance.onScreenPoints();
        gameController.instance.rounds();
        

        if (gameController.enemyScore == 2)
        {
            gameController.instance.doReset();
            StartCoroutine(resetCharacters()); 
           
        }
        else
        {
            StartCoroutine(resetCharacters());
            
        }

    }
    //Resetting all the features of character.
    IEnumerator resetCharacters()
    {
        health = 100; //this was under allow movement
        playerHB.value = 100;
        enemyController.instance.enemyHealth = 100;
        enemyController.instance.enemyHB.value = 100;
        yield return new WaitForSeconds(4);

        anim.SetTrigger("idle");
        anim.ResetTrigger("knockout");
        gameController.allowMovement = true;

        
    }
}

