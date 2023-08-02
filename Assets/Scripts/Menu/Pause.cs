using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{

  public GameObject PauseMenu;
  public GameObject Game;
  public bool pause;
  public PlayerController pc;
  public GameObject Play;
  	PlayerInput controls;

  void Awake()
  { 
    controls = new PlayerInput();
    controls.Pause.PauseMenu.performed += ctx => Break(); 
  }


  
      void Break()
    {
      pc.isSleeping = true;
      PauseMenu.SetActive(true);
      EventSystem.current.SetSelectedGameObject(Play);
      
        
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




