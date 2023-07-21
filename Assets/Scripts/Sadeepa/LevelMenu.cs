using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    //array to hold level buttons

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        //for loop to remove the interactive property of buttons
        for(int i = 0; i<buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //for loop to enable the interactive property of buttons
        for (int i = 0; i<unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        // Stop the level sound
        if (levelSound.instance != null)
        {
            levelSound.instance.MusicSource.Play();
        }

        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }


}
