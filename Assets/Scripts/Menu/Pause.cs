using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{

  public GameObject PauseMenu;
  public GameObject Game;
  public bool pause;
  public PlayerController pc;
  
  	PlayerInput controls;
  void Awake()
  { 
    controls = new PlayerInput();
    controls.Pause.PauseMenu.performed += ctx => Break(); 
  }


  
      void Break()
    {
        
      PauseMenu.SetActive(true);
        
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




