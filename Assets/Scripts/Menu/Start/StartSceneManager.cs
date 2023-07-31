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
        hOffset.upwardOffset = 15f;

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
    Invoke("ZoomOutDelay",1);
    Invoke("PlayerActive",9);
    Invoke("LoadScene",13);
    Invoke("PlayerMove",5);

    
    
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
            float zoomOut = cam.m_Lens.OrthographicSize *1.0005f;
            cam.m_Lens.OrthographicSize = zoomOut;
            yield return null;
        }
    }

    IEnumerator OffSet()
    {
        while(hOffset.upwardOffset >10)
        {
            float offsetViewFinder = hOffset.upwardOffset *0.999991f;
            hOffset.upwardOffset = offsetViewFinder;
            yield return null; 
        } 

    }

    IEnumerator horizontalOffset()
    {
        while(hOffset.horizontalOffset>90)
        {
            float horizontalOffsetViewFinder = hOffset.horizontalOffset * 0.99925f;
            hOffset.horizontalOffset = horizontalOffsetViewFinder;
            yield return null;
        }
    }


    void LoadScene()
    {
        SceneManager.LoadScene("Final_Leveldesign");
    }

    
   void PlayerMove()
   {
    Player.GetComponent<Rigidbody2D>().velocity = new Vector2(Player.transform.position.x *-0.25f, Player.transform.position.y);
   }

}