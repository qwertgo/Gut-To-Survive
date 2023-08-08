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

    public GameObject Player;
    public PlayerController pc;
    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /*public void Exit()
    {
      StartCoroutine(Zoom());
      StartCoroutine(OffSet());
      SpikeExit.SetActive(true);
      Application.Quit();
      
    }
*/

    public void Restart()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

  public void Skip()
  {  
    Player.transform.position = new Vector2(732, Player.transform.position.y);   
    pc.ShowHighscore();

  }
}