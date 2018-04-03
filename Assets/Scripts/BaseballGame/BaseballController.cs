using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballController : MonoBehaviour
{
    // Minimum velocity allowed in both directions before the baseball is deactivated
    public float minVelocity;

    // Rigidbody2D
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}