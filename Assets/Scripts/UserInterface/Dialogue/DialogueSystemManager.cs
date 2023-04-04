using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Scenes
{
    public string scene;
    public string name;
    public string dialogue;
}

public class Stories
{
    public List<Scenes> story = new List<Scenes>();
}

public class DialogueSystemManager : MonoBehaviour
{
    private static DialogueSystemManager instance;

    public static DialogueSystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueSystemManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI storyText;
    public Image dialogueBoxBackground;
    public int dialogueBoxOffsetX;
    public int dialogueBoxOffsetY;
    public float typingSpeed;
    public float readingSpeed;
    public float responseSpeed;
    public GameObject player;

    private RectTransform textTransform;
    private RectTransform dialogueBoxBackgroundTransform;
    private int currentIndex;
    private InputManager inputManager;
    private List<Scenes> scenesList;

    private void Start()
    {
        inputManager = player.GetComponent<InputManager>();
        dialogueBoxBackgroundTransform = dialogueBoxBackground.GetComponent<RectTransform>();
        textTransform = storyText.GetComponent<RectTransform>();
        dialogueBoxBackgroundTransform.anchoredPosition = new Vector2(textTransform.anchoredPosition.x + dialogueBoxOffsetX, textTransform.anchoredPosition.y + dialogueBoxOffsetY);
        LoadStories("story.json");
    }

    private void Update()
    {
        if (inputManager.onFoot.Skip.triggered)
        {
            //StartCoroutine(DisplayDialogue(GetDialogue("First Scene"), typingSpeed, responseSpeed, readingSpeed));
        }
    }

    private void LoadStories(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            Stories stories = JsonUtility.FromJson<Stories>(jsonText);
            scenesList = stories.story;
        }
        else
        {
            Debug.LogError("Unable to load stories: " + fileName);
        }
    }

    private void DisplayNextLine()
    {
        if (scenesList != null && scenesList.Count > 0)
        {
            if (currentIndex < scenesList.Count)
            {
                Scenes currentScene = scenesList[currentIndex];
                storyText.text = currentScene.scene + "\n" + currentScene.name + " : " + currentScene.dialogue;
                currentIndex++;
            }
            else
            {
                // End of scenes
                Debug.Log("End of scenes");
            }
        }
        else
        {
            Debug.LogError("Unable to display next line: scenesList is null or empty.");
        }
    }

    public IEnumerator DisplayDialogue(List<string> dialogues, float typingSpeed, float responseSpeed, float readingSpeed)
    {
        foreach (string dialogue in dialogues)
        {
            int colonIndex = dialogue.IndexOf(':');
            string name;
            if (colonIndex >= 0)
            {
                name = dialogue.Substring(0, colonIndex + 1);
            }
            else
            {
                name = "";
                Debug.LogError("No colon found in string.");
            }

            string dialogueWithoutName = dialogue.Replace(name, "");
            string[] sentences = dialogueWithoutName.Split("\n");
            dialogueBoxBackground.enabled = true;
            foreach (string sentence in sentences)
            {
                storyText.text = name;
                foreach (char letter in sentence)
                {
                    storyText.text += letter;
                    dialogueBoxBackgroundTransform.sizeDelta = new Vector2(storyText.preferredWidth, dialogueBoxBackgroundTransform.sizeDelta.y);
                    yield return new WaitForSeconds(typingSpeed);
                }
                yield return new WaitForSeconds(readingSpeed);
            }
            yield return new WaitForSeconds(responseSpeed);
        }
        storyText.text = "";
        dialogueBoxBackground.enabled = false;
    }

    public List<string> GetDialogue(string sceneName)
    {
        List<string> dialogues = new();
        foreach (Scenes scene in scenesList)
        {
            if (scene.scene.Equals(sceneName))
            {
                dialogues.Add($"{scene.name}: {scene.dialogue}");
            }
        }
        if (dialogues.Count > 0)
        {
            return dialogues;
        }
        else
        {
            Debug.LogError("GetDialogue: Cannot find scene!");
            return null;
        }
    }
}
