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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerTalking == false)
        {
            ShowDialogue();
            lineNum++;
            UpdateUI();
        }
        else if (Input.GetMouseButtonDown(1) && playerTalking == false)
        {
            if (lineNum != 0)
            {
                lineNum--;
                ShowDialogue();
            }
            UpdateUI();
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
            currSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
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

    void UpdateUI()
    {
        if (!playerTalking)
        {
            ClearButtons();
        }
        nameBox.text = characterName;
        StartCoroutine(AnimateDialogue());
    }

    public IEnumerator AnimateDialogue()
    {
        int i = 0;
        dialogueBox.text = "";
        while (i < dialogue.Length)
        {
            dialogueBox.text += dialogue[i++];
            source.Play();
            yield return new WaitForSeconds(0.02f);
        }
    }

    void ClearButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            print("Clearing buttons");
            Button b = buttons[i];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }

}

