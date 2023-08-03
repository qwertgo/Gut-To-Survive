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

        List<HighScoreEntry> highscoreList = SaveSystem.LoadHighscore();

        highscoreList.Add(new HighScoreEntry(pc.deathCount, PlayerController.playerName, pc.collectables, pc.time));
        highscoreList = BubbleSort(highscoreList);

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

        float minutes = Mathf.FloorToInt(entry.time / 60);
        float seconds = Mathf.FloorToInt(entry.time % 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);

        textArray[4].text = time;
    }

    List<HighScoreEntry> BubbleSort(List<HighScoreEntry> highscorelist)
    {
        //sort for deaths
        int runIndex = highscorelist.Count - 1 ;
        for(int i = highscorelist.Count -1; i > 0; i--)
        {
            int o = i - 1;
            if(highscorelist[i].deathCount <= highscorelist[o].deathCount)
            {
                highscorelist = Swap(highscorelist, i, o);
                runIndex--;
            }
            else
                break;
        }

        //sort for collectables
        for(int i = runIndex; i < highscorelist.Count - 1; i++)
        {
            int o = i + 1;
            if (highscorelist[i].deathCount == highscorelist[o].deathCount && highscorelist[i].collectables < highscorelist[o].collectables)
            {
                highscorelist = Swap(highscorelist, i, o);
                runIndex++;
            }
            else
                break;
        }

        //sort for time
        for(int i = runIndex; i < highscorelist.Count -1; i++)
        {
            int o = i + 1;
            if(highscorelist[i].deathCount == highscorelist[o].deathCount && highscorelist[i].collectables == highscorelist[o].collectables && highscorelist[i].time > highscorelist[o].time)
            {

            }
        }

        return highscorelist;
    }

    List<HighScoreEntry> Swap(List<HighScoreEntry> highscorelist,int i, int o)
    {
        HighScoreEntry tmp = highscorelist[i];
        highscorelist[i] = highscorelist[o];
        highscorelist[o] = tmp;
        return highscorelist;
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
        this.time = time;
        
    }

    public string NewToString()
    {
        return $"name: {name}, deaths: {deathCount}, collectables: {collectables}, time: {time}";
    }

    public int deathCount { get; private set; }
    public string name { get; private set; }
    public int collectables { get; private set; }
    public float time { get; private set; }
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

