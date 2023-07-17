using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;


     void Awake()
    {
       
        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>(){
            new HighScoreEntry{score = 568, name ="AAA"},
            new HighScoreEntry{score = 468, name ="LISA"},
            new HighScoreEntry{score = 683, name ="SAMU"},
            new HighScoreEntry{score = 678, name ="LENA"},
            new HighScoreEntry{score = 144, name ="ANN"},
            new HighScoreEntry{score = 924, name ="MÄXL"},
            new HighScoreEntry{score = 354, name ="ANI"},
        };

        for(int i=0; i<highscoreEntryList.Count; i++)
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

        highscoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highscoreEntry in highscoreEntryList){
            newEntry(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList};
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
      
    }
    private void newEntry(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList )
    {
            float temheight = 150f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            Transform _entryTransform = entryTransform.GetComponent<Transform>();
            
            	_entryTransform.transform.localPosition = new Vector2(_entryTransform.localPosition.x,-temheight  *0.2f *transformList.Count);
                Debug.Log( _entryTransform.transform.localPosition);
                _entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count +1;
            string rankString;
            switch(rank)
            {
             

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;   
                default:
                rankString = rank + "TH"; break;
            }

            entryTemplate.Find("PositionTex").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = highscoreEntry.score;
            entryTemplate.Find("DeathsTex").GetComponent<TextMeshProUGUI>().text =score.ToString();
        
          
            string name =highscoreEntry.name;
            entryTemplate.Find("NameTex").GetComponent<TextMeshProUGUI>().text =name;

           entryTemplate.gameObject.SetActive(true);

           transformList.Add(entryTransform);
    }
    
    private class Highscores{
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score; 
        public string name;
    }
}
