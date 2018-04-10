using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            movespeed *= 2.5f;
            criticalHealth = true;
            spriteRenderer.color = Color.red;
        }
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