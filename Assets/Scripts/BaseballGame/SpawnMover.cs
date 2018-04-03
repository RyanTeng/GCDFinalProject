using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMover : MonoBehaviour
{
    public float speed;
    public float time;
    public bool negative_direction;
    public bool sideways;
    private bool movepositive;
    private bool keepMoving = true;

    private Vector3 positiveDirection;
    private Vector3 negativeDirection;

    // Use this for initialization
    void Start()
    {
        if (negative_direction)
        {
            speed *= -1;
        }

        positiveDirection = new Vector3(0, speed * Time.deltaTime, 0);
        negativeDirection = new Vector3(0, -speed * Time.deltaTime, 0);

        if (sideways)
        {
            positiveDirection = new Vector3(speed * Time.deltaTime, 0, 0);
            negativeDirection = new Vector3(-speed * Time.deltaTime, 0, 0);
        }


        StartCoroutine(moveswitch());
    }

    // Update is called once per frame
    void Update()
    {
        if (movepositive)
        {
            transform.Translate(positiveDirection);
        }
        else
        {
            transform.Translate(negativeDirection);
        }

        print(transform.position);
    }

    IEnumerator moveswitch()
    {
        while (keepMoving)
        {
            if (movepositive)
            {
                movepositive = false;
            }
            else
            {
                movepositive = true;
            }

            yield return new WaitForSeconds(time);
        }
    }
}