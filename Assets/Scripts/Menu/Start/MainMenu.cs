using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject controlsMenu;
    [SerializeField] PlayerController pc;
    [SerializeField] cameraViewFinder camfind;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject letsGoButton;
    [SerializeField] GameObject volume;
    [SerializeField] AudioMixer audioMixer;

    

      // Start is called before the first frame update
   
   public void Start()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }

    public void PressedSaveNameButton()
    {
        pc.WalkLeft();

        Invoke("ShowControls", 2.5f);
    }

    void ShowControls()
    {
        controlsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(letsGoButton);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Final_Leveldesign");
    }

    public void PressedSettingsButton()
    {
        EventSystem.current.SetSelectedGameObject(volume);
    }

     public void PressedBackButton()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}