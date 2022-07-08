using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float xPos, yPos;
    public float leftWall, rightWall, topWall, bottomWall;
    static float defaultSpeed = 0.020f; //fix this to change players initial speed
    public float speed;
    Vector3 characterScale;
    float characterScaleX;
    private Animator ani;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    // Update is called once per frame
    void Update()
    {   
        //character speed is slowed down, relative to how many broccoli is being carried
        speed = defaultSpeed * Mathf.Pow(0.99f, this.transform.childCount);

        if (PlayerPrefs.GetInt("Paused") == 0) //paused?
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (xPos > leftWall) //stays within screen
                {
                    characterScale.x = -characterScaleX; //flip sprite
                    xPos -= speed;
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (xPos < rightWall)
                {
                    characterScale.x = characterScaleX; //flip sprite
                    xPos += speed;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (yPos < topWall)
                {
                    yPos += speed;
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (yPos > bottomWall)
                {
                    yPos -= speed;
                }
            }

            transform.position = new Vector3(xPos, yPos, transform.position.z);
            transform.localScale = characterScale;

            //animation triggers
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                ani.SetTrigger("run");
            if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
                ani.SetTrigger("idle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //When players bumps into an obstacle
        if (collision.collider.transform.name.Contains("Obstacle"))
        {
            source = collision.collider.transform.GetComponent<AudioSource>();

            source.Play();

            if (transform.childCount > 0)
            {
                //chooses a random number no more than half of the broccoli to drop
                int scatter = Random.Range(0, transform.childCount/2);
                for (int i = 0; i < scatter; i++)
                {
                    GameObject scatterChild;
                    int randomChild = Random.Range(0, transform.childCount);
                    
                    //scatter the broccolis
                    if (randomChild < transform.childCount)
                    {
                        scatterChild = transform.GetChild(randomChild).gameObject;
                        scatterChild.GetComponent<Collider2D>().isTrigger = false;
                        scatterChild.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        scatterChild.GetComponent<Rigidbody2D>().AddForce(this.transform.position, ForceMode2D.Impulse); 
                        scatterChild.transform.parent = GameObject.Find("Broccolis").transform;
                    }
                }
            }
        }
    }

}
