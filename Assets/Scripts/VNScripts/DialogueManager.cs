using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//from http://www.indiana.edu/~gamedev/2015/09/27/creating-a-visual-novel-in-unity/
public class DialogueManager : MonoBehaviour
{


    DialogueParser parser;

    public string dialogue, characterName;
    public int lineNum;
    int pose;
    string position;
    string[] options;
    public bool playerTalking;
    List<Button> buttons = new List<Button>();

    public Text dialogueBox;
    public Text nameBox;
    public GameObject choiceBox;
    private AudioSource source;
	private bool isCoroutine;
    private bool skip;



    // Use this for initialization
    void Start()
    {
        dialogue = "";
        characterName = "";
        pose = 0;
        position = "L";
        playerTalking = false;
        parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
        lineNum = 0;
        source = GetComponent<AudioSource>();
		isCoroutine = false;
        nameBox.text = "";
        dialogueBox.text = "";
        skip = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
            UpdateUI(true);
			
        }
		else if (Input.GetMouseButtonDown(1))
        {
            if (lineNum > 0)
            {
                UpdateUI(false);

            }
        }
        
    }

    public void ShowDialogue()
    {
        ResetImages();
        ParseLine();
    }


    void ResetImages()
    {
        if (characterName != "")
        {
            GameObject character = GameObject.Find(characterName);
            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = null;
        }
    }

    void ParseLine()
    {
        if (parser.GetName(lineNum) == "EndScene")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (parser.GetName(lineNum) != "Player")
        {
            playerTalking = false;
            characterName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            DisplayImages();
        }
        else
        {
            playerTalking = true;
            characterName = "";
            dialogue = "";
            pose = 0;
            position = "";
            options = parser.GetOptions(lineNum);
            CreateButtons();
        }
    }

    void DisplayImages()
    {
        if (characterName != "")
        {
            GameObject character = GameObject.Find(characterName);

            SetSpritePositions(character);

            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = character.GetComponent<CharacterPoses>().characterPoses[pose];
        }
    }

    void SetSpritePositions(GameObject spriteObj)
    {
        if (position == "L")
        {
            spriteObj.transform.position = new Vector3(-400, 0);
        }
        else if (position == "R")
        {
            spriteObj.transform.position = new Vector3(400, 0);
        }
        spriteObj.transform.position = new Vector3(spriteObj.transform.position.x, spriteObj.transform.position.y, 0);
    }

    void CreateButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(choiceBox);
            Button b = button.GetComponent<Button>();
            ChoiceButton cb = button.GetComponent<ChoiceButton>();
            cb.SetText(options[i].Split(':')[0]);
            cb.option = options[i].Split(':')[1];
            cb.box = this;
            b.transform.SetParent(this.transform);
            b.transform.localPosition = new Vector3(0, -25 + (i * 50));
            b.transform.localScale = new Vector3(1, 1, 1);
            buttons.Add(b);
        }
    }

    void UpdateUI(bool leftClick)
    {
        if (!playerTalking)
        {
            ClearButtons();
        }


		if (!isCoroutine) {
            if (leftClick)
            {
                if (skip)
                {
                    lineNum++;
                    skip = false;
                }
                ShowDialogue();
                nameBox.text = characterName;
                lineNum++;
            }
            else
            {
                nameBox.text = characterName;
                lineNum--;
                ShowDialogue();
                skip = true;
            }
			StartCoroutine ("AnimateDialogue");
		} 
		else if (isCoroutine) 
		{
			StopCoroutine("AnimateDialogue");
			dialogueBox.text = dialogue;
			isCoroutine = false;
		}
    }

    public IEnumerator AnimateDialogue()
    {
		isCoroutine = true;
        int i = 0;
        dialogueBox.text = "";
        while (i < dialogue.Length)
        {
            dialogueBox.text += dialogue[i++];
			source.pitch = Random.Range (0.75f, 1.25f);
            source.Play();
            yield return new WaitForSeconds(0.0175f);
        }
		isCoroutine = false;
    }

    void ClearButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Button b = buttons[i];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }

}

