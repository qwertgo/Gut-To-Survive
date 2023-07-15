using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
  public GameObject Game;
  public GameObject PauseMenu;



  
     void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
      {Game.SetActive(false);
      PauseMenu.SetActive(true);
    }}



}
