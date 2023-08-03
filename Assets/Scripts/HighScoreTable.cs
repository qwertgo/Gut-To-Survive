using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.InputSystem;



public class HighScoreTable : MonoBehaviour
{

    [SerializeField] float controllerScrollSpeed;
    private int minCharAmount = 3;
    private int maxCharAmount = 6;
    const string glyphs = "0123456789";
    string myString;

    PlayerInput input;

    Vector2 scrollDirection;


    [Header("References")]
    [SerializeField] PlayerController pc;
    [SerializeField] GameObject entryPrefab;
    [SerializeField] GameObject highscorePanel;

    RectTransform highscorePanelTransform;


    void Awake()
    {
        input = new PlayerInput();
        highscorePanelTransform = highscorePanel.GetComponent<RectTransform>();

        int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            myString += glyphs[Random.Range(0, glyphs.Length)];
        }

        List<HighScoreEntry> highscoreList = SaveSystem.LoadHighscore();
        highscoreList = new List<HighScoreEntry>();

        highscoreList.Add(new HighScoreEntry(pc.deathCount, myString, pc.collectables, pc.time));
        for(int i =0; i < 20; i++)
        {
            highscoreList.Add(new HighScoreEntry(i + 5, "coller Namer", 5, 128));
        }

        SaveSystem.SaveHighscore(highscoreList);

        for(int i = 0; i < highscoreList.Count; i++)
        {
            CreateEntryVisuals(highscoreList[i], i + 1);
        }

        highscorePanelTransform.sizeDelta = new Vector2(highscorePanelTransform.sizeDelta.x, highscoreList.Count * 33);
    }

    private void Update()
    {
        highscorePanelTransform.position += new Vector3(0, Time.deltaTime * controllerScrollSpeed * scrollDirection.y, 0);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovement;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovement;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        scrollDirection = -context.ReadValue<Vector2>();
    }

    void OnMovementCancelled(InputAction.CallbackContext c)
    {
        scrollDirection = Vector2.zero;
    }

    void CreateEntryVisuals(HighScoreEntry entry, int place)
    {
        GameObject entryGo = Instantiate(entryPrefab, highscorePanel.transform);

        TextMeshProUGUI[] textArray = entryGo.GetComponentsInChildren<TextMeshProUGUI>();

        textArray[0].text = place.ToString();
        textArray[1].text = entry.name;
        textArray[2].text = entry.deathCount.ToString();
        textArray[3].text = entry.collectables.ToString();
        textArray[4].text = entry.time.ToString();
    }
}

[System.Serializable]
class HighScoreEntry
{
    public HighScoreEntry(int deathCount, string name, int collectables, float time)
    {
        this.deathCount = deathCount;
        this.name = name;
        this.collectables = collectables;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        this.time = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int deathCount { get; private set; }
    public string name { get; private set; }
    public int collectables { get; private set; }
    public string time { get; private set; }
}

static class SaveSystem
{

    static string dataPath = Application.persistentDataPath + "highscore.bin";
    public static void SaveHighscore(List<HighScoreEntry> highscoreList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(dataPath, FileMode.Create);
        formatter.Serialize(stream, highscoreList);

        stream.Close();
    }


    public static List<HighScoreEntry> LoadHighscore()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(dataPath, FileMode.Open);

            List<HighScoreEntry> list = formatter.Deserialize(stream) as List<HighScoreEntry>;
            stream.Close();

            return list;
        }
        else
        {
            //Debug.Log("did not find file");
            return new List<HighScoreEntry>();
        }
    }
}

