using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {
	public GameObject s_player;
    public GameObject music;
    public GameObject manager;
    public GameObject image;
    public GameObject BG;
	public Sprite boy;
	public Sprite girl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
			s_player.SetActive (true);
			s_player.GetComponent<SpriteRenderer> ().sprite = boy;
            music.SetActive(true);
            manager.SetActive(true);
            BG.SetActive(true);
            image.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
			s_player.SetActive (true);
			s_player.GetComponent<SpriteRenderer> ().sprite = girl;
            music.SetActive(true);
            manager.SetActive(true);
            BG.SetActive(true);
            image.SetActive(false);
        }
    }
}
