using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class leaderboardManager : MonoBehaviour
{
    private int overrallStartCount;

    public static leaderboardManager Instance { get; private set; }
    
    private string completedLevels;

    public Text overallStarsText, overallStarsText2;

    //int overallStars;
    
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        completedLevels = PlayerPrefs.GetString("completedLevels");
        overrallStartCount = PlayerPrefs.GetInt("totalStars");
    
    }

    public void updateStartCount(){
        overrallStartCount +=1;
        PlayerPrefs.SetInt("totalStars",overrallStartCount);
    }

    public void setCollectedLevel(){
        completedLevels += SceneManager.GetActiveScene().buildIndex.ToString() +"/" ;
        PlayerPrefs.SetString("completedLevels",completedLevels);
    }

    public bool checkIfLevelDone(){
        string[] lvls = completedLevels.Split("/");
        List<string> temp = new List<string>();
        foreach(string i in lvls){
            temp.Add(i);
        }

        if(temp.Contains(SceneManager.GetActiveScene().buildIndex.ToString())){
            return true;
        }

        return false;
    }

    public void showInUI(){
        
        overallStarsText.text = overrallStartCount.ToString();
        overallStarsText2.text = overrallStartCount.ToString();


    }
}
