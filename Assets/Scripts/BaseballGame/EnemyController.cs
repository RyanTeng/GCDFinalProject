using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    private float movespeed;

    public float torque;
    public float knockback;

    public float upperspeed;
    public float lowerspeed;

    public int SCORE_KO;
    public int SCORE_HIT;

    private bool conscious;
    public int Health;

    private Rigidbody2D rb2d;
    private AudioSource sound;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();

        conscious = true;
        movespeed = Random.Range(lowerspeed, upperspeed);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (conscious)
        {
            rb2d.transform.position = Vector2.MoveTowards(rb2d.transform.position,
                player.GetComponent<Rigidbody2D>().transform.position, movespeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Baseball")
        {
            sound.Play();
            rb2d.AddTorque(torque);
            rb2d.AddForce(collision.attachedRigidbody.velocity.normalized * knockback);
            Health -= 5;
            player.GetComponent<PlayerController>().addToScore(SCORE_HIT);
            if (Health <= 0)
            {
                conscious = false;
                player.GetComponent<PlayerController>().addToScore(SCORE_KO);
                gameObject.SetActive(false);
            }
        }
    }
}