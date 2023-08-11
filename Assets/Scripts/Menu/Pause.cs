using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject volumeSlider;
    [SerializeField] Slider slider;
    [SerializeField] GameObject settingsButton;
    [SerializeField] PlayerController player;
    [SerializeField] AudioMixer audioMixer;
    bool pause;
    PlayerInput controls;

    void Awake()
    { 
        controls = new PlayerInput();
        controls.Pause.PauseMenu.performed += ctx => TogglePause();

        slider.onValueChanged.AddListener((v) =>
        {
            audioMixer.SetFloat("volume", v);
            Debug.Log(v);
        });
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
            player.enabled = true;
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(resumeButton);
            pause = true;
            Cursor.visible = true;
            player.enabled = false;
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void PressedSettingsButton()
    {
        EventSystem.current.SetSelectedGameObject(volumeSlider);
    }

    public void PressedBackButton()
    {
        EventSystem.current.SetSelectedGameObject(settingsButton);
    }
}




