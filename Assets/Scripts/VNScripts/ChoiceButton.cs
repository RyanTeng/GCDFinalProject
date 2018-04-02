using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

//from http://www.indiana.edu/~gamedev/2015/09/27/creating-a-visual-novel-in-unity/
public class ChoiceButton : MonoBehaviour
{

    public string option;
    public DialogueManager box;

    public void SetText(string newText)
    {
        this.GetComponentInChildren<Text>().text = newText;
    }

    public void SetOption(string newOption)
    {
        this.option = newOption;
    }

    public void ParseOption()
    {
        string command = option.Split(',')[0];
        string commandModifier = option.Split(',')[1];
        box.playerTalking = false;
        if (command == "line")
        {
            box.lineNum = int.Parse(commandModifier);
            box.ShowDialogue();
        }
        else if (command == "scene")
        {
            EditorSceneManager.LoadScene("Scene" + commandModifier);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
