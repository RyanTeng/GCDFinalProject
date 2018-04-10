using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject stella;
    public GameObject ryan;
    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        if (stella.active)
        {
            player = stella;
        } else {
            player = ryan;
        }
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
