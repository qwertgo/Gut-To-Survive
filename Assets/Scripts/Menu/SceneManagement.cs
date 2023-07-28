using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneManagement : MonoBehaviour
{
    public GameObject Game;
    public GameObject PauseMenu;
    public GameObject HighScoreTable; 
    public GameObject EndScreen; 
    public CinemachineVirtualCamera cam;
    public bool playerSleepin = true;
    public PlayerController pc;
    public cameraViewFinder camfind;
    public GameObject SpikeExit;
    public GameObject StartCanvas;
      // Start is called before the first frame update
    public void Update()
    {
        pc.isSleeping = playerSleepin;
    }
   
    public void ExitMenu()
    {
        Game.SetActive(true);
        PauseMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
        playerSleepin = true;
    }

    public void Exit()
    {
      StartCoroutine(Zoom());
      StartCoroutine(OffSet());
      SpikeExit.SetActive(true);
      Application.Quit();
      
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerSleepin = false;

    }

    public void Skip()
    {
        EndScreen.SetActive(true);
        HighScoreTable.SetActive(true);
        playerSleepin = true;

    }

    public void Play() 
    { 
        StartCoroutine(OffSet());
        StartCoroutine(Zoom());
        playerSleepin = false;
        StartCanvas.SetActive(false);
    }

    IEnumerator Zoom()
    {
        while(cam.m_Lens.OrthographicSize > 8)
        {   
            float zoom = cam.m_Lens.OrthographicSize *0.98f;
            cam.m_Lens.OrthographicSize = zoom;
            yield return null;
        }
    }

    IEnumerator OffSet()
    {
        while(camfind.upwardOffset > 0)
        {
            float offsetViewFinder = camfind.upwardOffset *0.89f;
            camfind.upwardOffset = offsetViewFinder;
            yield return null;
        } 
    }

    

}