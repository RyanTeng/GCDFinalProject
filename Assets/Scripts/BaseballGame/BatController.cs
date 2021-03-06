﻿using UnityEngine;

public class BatController : MonoBehaviour
{
    // Minimum velocity allowed in both directions before the baseball is deactivated
    public float minVelocity;

    // Rigidbody2D
    private Rigidbody2D rb;
    private AudioSource sound;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        sound.Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If the baseball comes to a stop, destroy it
        if (Mathf.Abs(rb.velocity.x) <= minVelocity && Mathf.Abs(rb.velocity.y) <= minVelocity)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //sound.Play();
            rb.velocity = -1 * rb.velocity;
        }
    }
}