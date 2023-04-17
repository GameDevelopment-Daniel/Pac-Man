using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class pacmanMoveScript : MonoBehaviour
{
    [SerializeField] 
    int score;
    TextMeshProUGUI scoreComp;
    bool shieldOn=false;

    [SerializeField]
    InputAction up = new InputAction(type: InputActionType.Button);
    [SerializeField]
    InputAction down = new InputAction(type: InputActionType.Button);
    [SerializeField]
    InputAction left = new InputAction(type: InputActionType.Button);
    [SerializeField]
    InputAction right = new InputAction(type: InputActionType.Button);

    [SerializeField] AudioSource powerUp;
    [SerializeField] AudioSource beep;
    [SerializeField] AudioSource loss;
    [SerializeField] AudioSource win;

    [SerializeField] int speed;
    Vector3 wallLeftPos;
    Vector3 wallRightPos;

    void Start()
    {
        score= -1;
        scoreComp=GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        wallLeftPos = GameObject.Find("wallLeft").GetComponent<Transform>().position;
        wallRightPos = GameObject.Find("wallRight").GetComponent<Transform>().position;

    }

    public void OnEnable()
    { 
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
    }

    public void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
    }
    void Update()
    {
        if (up.IsPressed()) //cant move more then 1 direction at a time
        {
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.position+= new Vector3( 0, speed * Time.deltaTime, 0);
            transform.eulerAngles = new Vector3(0,0,90);

        }
        else if(down.IsPressed())
        {
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.position += new Vector3(0, speed * Time.deltaTime* -1, 0);
            transform.eulerAngles = new Vector3(0,0,-90);

        }
        else if(left.IsPressed())
        {
            transform.position += new Vector3(speed * Time.deltaTime*-1,0, 0);
            if(transform.localScale.x>0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.eulerAngles = Vector3.zero;

        }
        else if(right.IsPressed()){
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.eulerAngles = Vector3.zero;

        }

    }
    // hit by a ghost and lost , touch a shield.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "ghost" && !shieldOn) //no shield and hit by a ghost
        {
            //upside down pacman
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            transform.eulerAngles = Vector3.zero;

            // enabled movment and collistion
            GetComponent<pacmanMoveScript>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            //show loss image,text
            GameObject.Find("lossImage").GetComponent<UnityEngine.UI.Image>().enabled= true;
            GameObject.Find("lossText").GetComponent<TextMeshProUGUI>().enabled = true;

            //sound lost
            loss.Play();
        }

        if(collision.tag== "shield") //got to a shield
        {
            Destroy(collision.gameObject);
            shieldOn = true;
            GameObject.Find("pacmanShield").GetComponent<SpriteRenderer>().enabled = true;
            powerUp.Play();//sound shield

        }
        //change side when got to the midelle of 1 of the sides
        if (collision.name == "wallLeft")
        {
            transform.position = wallRightPos + new Vector3(-0.55f, 0, 0);
        }
        else if (collision.name == "wallRight")
        {           
            transform.position = wallLeftPos + new Vector3(0.55f, 0, 0);
        }

    }
    // eat a star
    private void OnTriggerStay2D(Collider2D other)
    {
       // remove the star and +1 to score.
        if (other.tag == "star")
        {
            Destroy(other.gameObject);
            score++;
            scoreComp.text = score.ToString();
            beep.Play(); //sound eat star

            //50 score and won the game
            if (score == 80)
            {
                // enabled movment and collistion
                GetComponent<pacmanMoveScript>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                //show won image,text
                GameObject.Find("wonImage").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("wonText").GetComponent<TextMeshProUGUI>().enabled = true;

                //sound won
                win.Play();
            }
        }
    }
    //remove the shield after hit by a ghost

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "ghost" && shieldOn)
        {
            shieldOn = false;
            GameObject.Find("pacmanShield").GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
