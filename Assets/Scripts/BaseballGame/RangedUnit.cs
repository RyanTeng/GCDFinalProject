using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : MonoBehaviour {

    private GameObject player;

    private float movespeed;

    public float torque;
    public float knockback;

    public float upperspeed;
    public float lowerspeed;

    private bool conscious;
    public int Health;
    public int minRange;

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
            if (target == null) return;
            transform.LookAt(target);
            float distance = Vector3.Distance(transform.position, target.position);
            bool tooClose = distance < minRange;
            Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
            transform.Translate(direction * Time.deltaTime);
        }
    }

    private Transform target = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") target = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Baseball")
        {
            sound.Play();
            rb2d.AddTorque(torque);
            rb2d.AddForce(collision.attachedRigidbody.velocity.normalized * knockback);
            print("Hit");
            Health -= 5;
            print(Health);
            if (Health <= 0)
            {
                conscious = false;
                gameObject.SetActive(false);
            }
        }
    }
}
