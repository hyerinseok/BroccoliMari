using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broccoli : MonoBehaviour
{
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Player comes in contact with broccoli : broccoli sticks to player
        if (collision.collider.name == "Player")
        {
            source.Play();
            transform.GetComponent<Collider2D>().isTrigger = true;
            transform.parent = GameObject.Find("Player").transform;
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void Update()
    {
        //keep broccolis near Player
        if(transform.parent.name == "Player")
        {
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
    }
}