using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    // private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private string adjectives;
    private int minCharAmount = 3;
    private int maxCharAmount = 6;
    public const string glyphs = "0123456789";
    public string myString;
    public PlayerController pc;
    public int remove;

    PlayerInput controls;
    public ScrollRect scrollRect;
    public Transform ScrollContent;

    public float speed;

    

    void Start()
    {

        scrollRect = GetComponent<ScrollRect>();

    }


    void Awake()
    {
        controls = new PlayerInput();
        controls.Pause.ScrollUp.performed += ctx => ScrollUp();
        controls.Pause.ScrollDown.performed += ctx => ScrollDown();

        int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            myString += glyphs[Random.Range(0, glyphs.Length)];
        }

        HighscoreData data = SaveSystem.LoadHighscore();

        data.highscoreList.Add(new HighScoreEntry(pc.deathCount - pc.revive, myString));

        //AddEntry(pc.deathCount - pc.revive, myString);

        entryTemplate.gameObject.SetActive(false);


        //highScoreEntryList manuelle Eingabe
        /* highscoreEntryList = new List<HighScoreEntry>()
             {
                 new HighScoreEntry{ score =3, name ="1234"},
                 new HighScoreEntry{ score =1, name ="1234"},
                 new HighScoreEntry{ score = 4, name ="ABC"},
                 new HighScoreEntry{score= 7, name = "5962" },
                 new HighScoreEntry{ score =9, name ="Lisa"},
                 new HighScoreEntry{ score =56, name ="Ann"},
                 new HighScoreEntry{ score = 86, name ="Lena"},
                 new HighScoreEntry{score= 103, name = "Samu" },
                 new HighScoreEntry{ score =72, name ="Lisa"},
                 new HighScoreEntry{ score =63, name ="Ann"},
                 new HighScoreEntry{ score = 117, name ="Lena"},
                 new HighScoreEntry{score= 59, name = "Samu" },
                 new HighScoreEntry{ score =99, name ="Ann"},
                 new HighScoreEntry{ score = 86, name ="Lena"},
                 new HighScoreEntry{score= 112, name = "Samu" },
                 new HighScoreEntry{ score =20, name ="Lisa"},
                 new HighScoreEntry{ score =63, name ="Ann"},
                 new HighScoreEntry{ score = 164, name ="Lena"},
                 new HighScoreEntry{score= 15, name = "Samu" },
             };

             Highscores highscores = new Highscores{highscoreEntryList = highscoreEntryList};       
             string json = JsonUtility.ToJson(highscores);
             PlayerPrefs.SetString("highscoreTable", json);
             PlayerPrefs.Save();
             Debug.Log(PlayerPrefs.GetString("highscoreTable"));*/



        //Sort entry list by Score
        /*  for(int i=0; i< highscoreEntryList.Count; i++)
           {
               for(int j = i+1; j< highscoreEntryList.Count; j++)
               {
                   if(highscoreEntryList[j].score > highscoreEntryList[i].score)
                   {
                       HighScoreEntry tmp = highscoreEntryList[i];
                       highscoreEntryList[i] = highscoreEntryList[j];
                       highscoreEntryList[j] = tmp; 
                   }
               }
           }
   */

        //string jsonString = PlayerPrefs.GetString("highscoreTable");

        //HighscoreData highscores = JsonUtility.FromJson<HighscoreData>(jsonString);

        for (int i = 0; i < data.highscoreList.Count; i++)
        {
            for (int j = i + 1; j < data.highscoreList.Count; j++)
            {
                if (data.highscoreList[j].score < data.highscoreList[i].score)
                {
                    HighScoreEntry tmp = data.highscoreList[i];
                    data.highscoreList[i] = data.highscoreList[j];
                    data.highscoreList[j] = tmp;
                }
            }
        }

        SaveSystem.SaveHighscore(data);

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in data.highscoreList)
        {
            newEntry(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }



    private void newEntry(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float temheight = 150f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        Transform _entryTransform = entryTransform.GetComponent<Transform>();

        _entryTransform.transform.localPosition = new Vector2(_entryTransform.localPosition.x, -temheight * 0.2f * transformList.Count);
        //Debug.Log(_entryTransform.transform.localPosition);
        _entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default:
                rankString = rank + "TH"; break;
        }

        entryTemplate.Find("PositionTex").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTemplate.Find("DeathsTex").GetComponent<TextMeshProUGUI>().text = score.ToString();


        string name = highscoreEntry.name;
        entryTemplate.Find("NameTex").GetComponent<TextMeshProUGUI>().text = name;


        transformList.Add(_entryTransform);


    }

    public void AddEntry(int score, string name)
    {
        // Create
        HighScoreEntry highscoreEntry = new HighScoreEntry ( score = 3, name = "dfdf");

        //Load
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighscoreData highscores = JsonUtility.FromJson<HighscoreData>(jsonString);

        //Add
        Debug.Log(highscores);
        Debug.Log(highscores.highscoreList);
        Debug.Log(highscoreEntry);
        highscores.highscoreList.Add(highscoreEntry);

        //Save
        for (int i = 1; i > highscores.highscoreList.Count; i++)
        {
            for (int j = i + 1; j > highscores.highscoreList.Count; j++)
            {
                if (highscores.highscoreList[j].score < highscores.highscoreList[i].score)
                {
                    HighScoreEntry tmp = highscores.highscoreList[i];
                    highscores.highscoreList[i] = highscores.highscoreList[j];
                    highscores.highscoreList[j] = tmp;
                }
            }
        }
        if (highscores.highscoreList.Count > remove)
        {
            for (int h = highscores.highscoreList.Count; h > remove; h--)
            {
                highscores.highscoreList.RemoveAt(remove);
            }
        }
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }




    void ScrollUp()
    {
        Debug.Log("Input");
        ScrollContent.transform.position = new Vector2(ScrollContent.transform.position.x, ScrollContent.transform.position.y * 0.75f);
    }

    void ScrollDown()
    {
        Debug.Log("Input");
        ScrollContent.transform.position = new Vector2(ScrollContent.transform.position.x, ScrollContent.transform.position.y * 1.25f);

    }


    void OnEnable()
    {
        controls.Pause.Enable();
    }

    void OnDisable()
    {
        controls.Pause.Disable();
    }

    

    
}

[System.Serializable]
class HighscoreData
{
    public List<HighScoreEntry> highscoreList = new List<HighScoreEntry>();
}

[System.Serializable]
class HighScoreEntry
{
    public HighScoreEntry(int score, string name)
    {
        this.score = score;
        this.name = name;
    }

    public int score;
    public string name;
}

static class SaveSystem
{

    static string dataPath = Application.persistentDataPath + "highscore.bin";
    public static void SaveHighscore(HighscoreData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(dataPath, FileMode.Create);
        formatter.Serialize(stream, data.highscoreList);

        stream.Close();
    }


    public static HighscoreData LoadHighscore()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(dataPath, FileMode.Open);

            List<HighScoreEntry> list = formatter.Deserialize(stream) as List<HighScoreEntry>;
            HighscoreData d = new HighscoreData();
            stream.Close();

            d.highscoreList = list;
            return d;

        }
        else
        {
            Debug.Log("did not find file");
            return new HighscoreData();
        }
    }
}

