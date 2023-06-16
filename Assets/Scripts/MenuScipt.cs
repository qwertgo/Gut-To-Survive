
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScipt : MonoBehaviour
{
    public void StartLevelScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
