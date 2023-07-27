using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class end : MonoBehaviour
{
public GameObject Highscore;
public bool HA = false;
public GameObject EndScreen;

 


    // Start is called before the first frame update
   public void OnCollisionEnter2D(Collision2D coll)
    {
        coll.gameObject.CompareTag("Player");
        {
        HA = true;
        EndScreen.SetActive(true);
        Highscore.SetActive(true);

        }
        
    }
}

