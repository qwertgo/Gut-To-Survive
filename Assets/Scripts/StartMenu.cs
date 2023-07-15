using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("Final_Leveldesign");
    }
    
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}