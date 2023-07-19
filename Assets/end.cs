using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
public GameObject Highscore;
public bool HA = false;

 

    // Start is called before the first frame update
   public void OnCollisionEnter2D(Collision2D coll)
    {
        coll.gameObject.CompareTag("Player");
        {
        HA = true;
        Debug.Log(HA);
        Highscore.SetActive(true);      
        }
        
    }
}
