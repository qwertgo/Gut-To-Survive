using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{

  public GameObject PauseMenu;
  public GameObject Game;
  public bool pause;
  

  
     void Update()
    {
      
      pause = false;
      
      if(Input.GetKey(KeyCode.Escape) && pause != true) 
      {
        PauseMenu.SetActive(true);
     /*   Find("
        Time.timeScale=0;
        pause = true;

       */
      }
    
     
    }
}




