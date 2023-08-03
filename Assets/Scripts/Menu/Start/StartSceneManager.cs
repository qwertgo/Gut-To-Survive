using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;
using UnityEngine.Audio;
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
    string first;
    string second; 
    string third; 
    string fourth;
    string PlayerName; 
    public GameObject Name;
    public GameObject firstLetter;
    public GameObject secondLetter;
    public GameObject thirdLetter;
    public GameObject fourthLetter;
    public GameObject PlayButton;
    public GameObject Volume;
        public AudioMixer audioMixer;

    

      // Start is called before the first frame update
   
   public void Start()
    {
        cam.m_Lens.OrthographicSize = 10;
        hOffset.horizontalOffset = 179;
        hOffset.upwardOffset = 15f;
        EventSystem.current.SetSelectedGameObject(PlayButton);


    }
 
    public void Save()
    {
        first = firstLetter.GetComponent<TextMeshProUGUI>().text;
        second = secondLetter.GetComponent<TextMeshProUGUI>().text;
        third = thirdLetter.GetComponent<TextMeshProUGUI>().text;
        fourth = fourthLetter.GetComponent<TextMeshProUGUI>().text;

        PlayerName = first + second + third + fourth;
        Debug.Log(PlayerName);
        SceneManager.LoadScene("Final_Leveldesign");


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
    Player.transform.Rotate(0.0f,180f,0.0f); 
    StartCanvas.SetActive(false);
    fadePlay.Invoke("Fade",6);
    Invoke("ZoomOutDelay",1);
    Invoke("PlayerActive",9);
    Invoke("PlayerMove",5);    
    Invoke("NameSelect",7);
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
    pc.isSleeping = false;
    if(!pc.isSleeping)
    pc.CrossFade("StartWalk");
    pc.walkVelocityX = -1;
   }

   void NameSelect()
   {
    Name.SetActive(true);
   }

    public void Settings()
    {
        EventSystem.current.SetSelectedGameObject(Volume);
    }

     public void Back()
    {
        EventSystem.current.SetSelectedGameObject(PlayButton);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}