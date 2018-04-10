using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {
    public GameObject stella;
    public GameObject ryan;
    public GameObject music;
    public GameObject manager;
    public GameObject image;
    public GameObject BG;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ryan.SetActive(true);
            music.SetActive(true);
            manager.SetActive(true);
            BG.SetActive(true);
            image.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            stella.SetActive(true);
            music.SetActive(true);
            manager.SetActive(true);
            BG.SetActive(true);
            image.SetActive(false);
        }
    }
}
