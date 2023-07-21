using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
#if UNITY_STANDALONE
    Application.Quit();
#endif


#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
