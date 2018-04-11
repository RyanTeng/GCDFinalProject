using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    public Sprite[] possibleSprites;


    private float movespeed;
    private bool criticalHealth;

    public float torque;
    public float knockback;

    public float upperspeed;
    public float lowerspeed;

        
    // The bat to instantiate
    public GameObject bat;
    // Bat Speeds
    public float torqueSpeed;
    public float batSpeed;
    
    public int Health;

    private Rigidbody2D rb2d;
    private AudioSource sound;

    

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randSpriteIndex = Random.Range(0, possibleSprites.Length);
        spriteRenderer.sprite = possibleSprites[randSpriteIndex];

        rb2d = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        movespeed = Random.Range(lowerspeed, upperspeed);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.transform.position = Vector2.MoveTowards(rb2d.transform.position,
            player.GetComponent<Rigidbody2D>().transform.position, movespeed * Time.deltaTime);
        if (!criticalHealth && Health < 30)
        {
            throwBat();
            movespeed *= 2.5f;
            criticalHealth = true;
            spriteRenderer.color = Color.red;
        }
    }
    
    // Instantiates a new ball and gives it a velocity and torque
    public void throwBat()
    {
        Vector3 direction = player.transform.position;
        // Instantiate ball
        GameObject newBat = Object.Instantiate(bat, rb2d.transform.position, Quaternion.identity);
        newBat.SetActive(true);
        // New ball Rigidbody
        Rigidbody2D newBatrb2d = newBat.GetComponent<Rigidbody2D>();
        Vector3 norm = player.transform.position - rb2d.transform.position;
        newBatrb2d.velocity = (norm.normalized * batSpeed);
        newBatrb2d.AddTorque(torqueSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Baseball"))
        {
            sound.Play();
            rb2d.AddTorque(torque);
            rb2d.AddForce(collision.attachedRigidbody.velocity.normalized * knockback);
            Health -= 5;
            movespeed += .05f;
            if (Health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}