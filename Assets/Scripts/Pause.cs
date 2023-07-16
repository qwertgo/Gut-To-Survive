using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
  public GameObject Game;
  public GameObject PauseMenu;
  public PlayerController pc;


  
     void Update()
    {
      if(Input.GetKey(KeyCode.Escape))
      {
        pc.enabled = false;
        Game.SetActive(false);
      PauseMenu.SetActive(true);
    }}



}
