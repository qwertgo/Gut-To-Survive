using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject Game;
    
    public CinemachineVirtualCamera cam;
    public bool playerSleepin;
    public PlayerController pc;
    public HorizontalOffset hOffset;
    public GameObject StartCanvas;
    public GameObject Ebene1;
    public FadePlay fadePlay;
    public cameraViewFinder camfind;
    public GameObject Player;

      // Start is called before the first frame update
   
   public void Start()
    {
        cam.m_Lens.OrthographicSize = 10;
        hOffset.horizontalOffset = 179;
        camfind.upwardOffset = 15f;

    }
 


    public void Exit()
    {
     
      Application.Quit();
      
    }

    public void Play() 
    { 
    pc.isSleeping = true;
    StartCoroutine(Zoom());
    StartCoroutine(OffSet());
    StartCoroutine(horizontalOffset());
    StartCanvas.SetActive(false);
    fadePlay.Invoke("Fade",6);
    StartCoroutine(ZoomOut());
    Invoke("PlayerActive",9);
    Invoke("LoadScene",13);

    
    
    }

    void PlayerActive()
    {
        Player.SetActive(false);
    }

    
    void ZoomOutDelay()
    {
        StartCoroutine(ZoomOut());
    }

    IEnumerator Zoom()
    {
        while(cam.m_Lens.OrthographicSize > 10)
        {   
            float zoom = cam.m_Lens.OrthographicSize *0.993f;
            cam.m_Lens.OrthographicSize = zoom;
            yield return null;
        }
    }

    IEnumerator ZoomOut()
    {
        while(cam.m_Lens.OrthographicSize < 25)
        {   
            float zoomOut = cam.m_Lens.OrthographicSize *1.0015f;
            cam.m_Lens.OrthographicSize = zoomOut;
            yield return null;
        }
    }

    IEnumerator OffSet()
    {
        while(camfind.upwardOffset >10)
        {
            float offsetViewFinder = camfind.upwardOffset *0.99f;
            camfind.upwardOffset = offsetViewFinder;
            yield return null; 
        } 

    }

    IEnumerator horizontalOffset()
    {
        while(hOffset.horizontalOffset>90)
        {
            float horizontalOffsetViewFinder = hOffset.horizontalOffset * 0.998f;
            hOffset.horizontalOffset = horizontalOffsetViewFinder;
            yield return null;
        }
    }


    void LoadScene()
    {
        SceneManager.LoadScene("Final_Leveldesign");
    }

    
   

}