using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

//from http://www.indiana.edu/~gamedev/2015/09/27/creating-a-visual-novel-in-unity/
public class DialogueParser : MonoBehaviour
{

    struct DialogueLine
    {
        public string name;
        public string content;
        public int pose;
        public string position;
        public string[] options;

        public DialogueLine(string Name, string Content, int Pose, string Position)
        {
            name = Name;
            content = Content;
            pose = Pose;
            position = Position;
            options = new string[0];
        }
    }

    List<DialogueLine> lines;

    // Use this for initialization
    void Start()
    {
        string file = "Assets/Dialogue/";
        string sceneName = EditorSceneManager.GetActiveScene().name;
        file += sceneName;
        file += ".txt";

        lines = new List<DialogueLine>();

        LoadDialogue(file);
    }

    void LoadDialogue(string filename)
    {
        string line;
        StreamReader r = new StreamReader(filename);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] lineData = line.Split(';');

                        DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]), lineData[3]);
                        lines.Add(lineEntry);
                }
            }
            while (line != null);
            r.Close();
        }
    }

    public string GetPosition(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].position;
        }
        return "";
    }

    public string GetName(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetContent(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].content;
        }
        return "";
    }

    public int GetPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].pose;
        }
        return 0;
    }

    public string[] GetOptions(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].options;
        }
        return new string[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
