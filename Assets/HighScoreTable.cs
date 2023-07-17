using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;


     void Awake()
    {

        


       entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for(int i=0; i<10; i++)
        { 
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            Transform _entryTransform = entryTransform.GetComponent<Transform>();
            
            _entryTransform.transform.localPosition = new Vector2(_entryTransform.localPosition.x, _entryTransform.transform.localPosition.y * -7 *i);
           Debug.Log( _entryTransform.transform.localPosition);
           _entryTransform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
