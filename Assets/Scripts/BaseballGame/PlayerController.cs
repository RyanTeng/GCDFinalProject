using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text health_text;
    public Text score_text;
    private Rigidbody2D rb2d;
    private AudioSource sound;

    private bool boosted;
    private bool criticalHealth;

    // The baseball to instantiate
    public GameObject baseball;

    // Baseball Speeds
    public float torqueSpeed;
    public float ballSpeed;
    public int Health;
    private int score;

    // The maximum amount of baseballs the player can have
    public int maxBaseballCount;

    // The current amount of baseballs the player possesses
    private int curBaseballCount;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        score = 0;
        boosted = false;
        health_text.color = Color.white;
        health_text.text = "Health: " + Health.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float tempspeed = speed;
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (Input.GetKey(KeyCode.Space))
        {
            tempspeed *= 2;
        }
        
        rb2d.velocity = (movement * tempspeed);

        if (Input.GetMouseButtonDown(0))
        {
            spawnBall();
        }


        if (!criticalHealth && Health <= 25)
        {
            criticalHealth = true;
            health_text.color = Color.red;
            GameObject.FindWithTag("ActionMusic").GetComponent<AudioSource>().pitch = 1.15f;
        }
    }

    // Instantiates a new ball and gives it a velocity and torque
    public void spawnBall()
    {
        Vector3 direction = Input.mousePosition;
        // Instantiate ball
        GameObject newBall = Object.Instantiate(baseball, rb2d.transform.position, Quaternion.identity);
        newBall.SetActive(true);
        // Lower the count
        curBaseballCount--;
        // New ball Rigidbody
        Rigidbody2D newBallrb2d = newBall.GetComponent<Rigidbody2D>();
        Vector3 norm = Camera.main.ScreenToWorldPoint(direction) - rb2d.transform.position;
        newBallrb2d.velocity = (norm.normalized * ballSpeed);
        newBallrb2d.AddTorque(torqueSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            sound.Play();
            Health -= 5;
            health_text.text = "Health: " + Health.ToString();
            if (Health <= 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}