using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

	private SpriteRenderer SpriteRenderer;
	// Use this for initialization
	void Start ()
	{
		SpriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position += Vector3.forward;

	}
}
