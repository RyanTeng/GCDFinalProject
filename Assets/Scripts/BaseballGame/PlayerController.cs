using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb2d;
	private AudioSource sound;

	// The baseball to instantiate
	public GameObject baseball;
	
	// Baseball Speeds
	public float torqueSpeed;
	public float ballSpeed;
	public int Health;
	
	// The maximum amount of baseballs the player can have
	public int maxBaseballCount;
	// The current amount of baseballs the player possesses
	private int curBaseballCount;
	
	
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		sound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.velocity = (movement * speed);

		if (Input.GetMouseButtonDown(0))
		{
			spawnBall();
		}
	}
	
	// Instantiates a new ball and gives it a velocity and torque
	public void spawnBall()
	{
		print("click");
		Vector3 direction = Input.mousePosition;
		// Instantiate ball
		GameObject newBall = Object.Instantiate(baseball, rb2d.transform.position, Quaternion.identity);
		newBall.SetActive(true);
		// Lower the count
		curBaseballCount--;
		// New ball Rigidbody
		Rigidbody2D newBallrb2d= newBall.GetComponent<Rigidbody2D>();
		Vector3 norm = Camera.main.ScreenToWorldPoint (direction) - rb2d.transform.position;
		newBallrb2d.velocity = (norm.normalized*ballSpeed);
		print (direction);
		print (rb2d.transform.position);
		newBallrb2d.AddTorque(torqueSpeed);
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			sound.Play();
			Health -= 5;
			if (Health <= 0)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
