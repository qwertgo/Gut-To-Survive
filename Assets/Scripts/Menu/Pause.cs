using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject resumeButton;
    bool pause;
    PlayerInput controls;

  void Awake()
  { 
    controls = new PlayerInput();
    controls.Pause.PauseMenu.performed += ctx => TogglePause(); 
  }

    public void TogglePause()
    {
        if (pause)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            EventSystem.current.SetSelectedGameObject(null);
            pause = false;
            Cursor.visible = false;
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(resumeButton);
            pause = true;
            Cursor.visible = true;
        }
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




