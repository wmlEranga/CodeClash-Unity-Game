using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class next_level1 : MonoBehaviour
{

    public static next_level1 instance;
    //public PlayFabManager playfabManager;
    //int maxPlatform = 0;

    void Awake()
    {
        //playfabManager = FindObjectOfType<PlayFabManager>();
        //if (playfabManager == null)
        //{
        //    Debug.LogError("PlayFabManager not found in the scene.");
        //}
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //maxPlatform = 1;
            //playfabManager.SendLeaderboard(maxPlatform);
            UnlockNewLevel();
            SceneManager.LoadScene("Scenes/Level 2");
        }
    }
     

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
