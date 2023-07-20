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
    private string adjectives;
    private int minCharAmount = 3;
    private int maxCharAmount = 6;
    public const string glyphs= "0123456789";
    public string myString;
    public end End;
    public PlayerController pc;
    public int remove;
    

  
    void Awake()
    {   
     /*  int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
        for(int i=0; i<charAmount; i++)
        {
            myString += glyphs[Random.Range(0, glyphs.Length)];
         }
          
              
                
      /*  Debug.Log(pc.DeathCount);
        AddEntry(pc.DeathCount, myString);
        */

        entryTemplate.gameObject.SetActive(false);


     //highScoreEntryList manuelle Eingabe
     highscoreEntryList = new List<HighScoreEntry>()
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
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));


   
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
        }*/

        
       /*string jsonString = PlayerPrefs.GetString("highscoreTable");
        
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
          for(int i=0; i< highscores.highscoreEntryList.Count; i++)
        {
            for(int j = i+1; j< highscores.highscoreEntryList.Count; j++)
            {
                if(highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp; 
                }
            }
        }*/
        
        highscoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
        newEntry(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
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
        
          
            string name = highscoreEntry.name;
            entryTemplate.Find("NameTex").GetComponent<TextMeshProUGUI>().text =name;


        transformList.Add(_entryTransform);
        
    }
    
 /* public void AddEntry (int score, string name)
    {
        // Create
        HighScoreEntry highscoreEntry = new HighScoreEntry {score = score, name=name};

        //Load
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add
        highscores.highscoreEntryList.Add(highscoreEntry);
        
        //Save
        for(int i=1; i> highscores.highscoreEntryList.Count; i++)
        {
            for(int j = i+1; j> highscores.highscoreEntryList.Count; j++)
            {
                if(highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp; 
                }
            }
        }
         if (highscores.highscoreEntryList.Count > remove)
        {
            for (int h = highscores.highscoreEntryList.Count; h>remove; h--)
            {
                highscores.highscoreEntryList.RemoveAt(remove);
            }
        }
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
}
    */

private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score; 
        public string name;
    }

}

