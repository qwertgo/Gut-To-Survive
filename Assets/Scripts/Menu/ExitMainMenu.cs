using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMainMenu : MonoBehaviour
{
    public GameObject Game;
  public GameObject PauseMenu;
    // Start is called before the first frame update
    public void ExitMenu()
    {
        Game.SetActive(true);
        PauseMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }


  public void Restart()
    {
   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}