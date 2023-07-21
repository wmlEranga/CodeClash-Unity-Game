using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QUIT : MonoBehaviour
{
    public void BackGame()
    {
        if(levelSound.instance != null)
        {
            levelSound.instance.MusicSource.Pause();
        }
        SceneManager.LoadScene("Main Menu");
    }
}
