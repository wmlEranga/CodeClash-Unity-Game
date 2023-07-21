using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starCounter : MonoBehaviour
{
    private UIDocument _doc;
    public Label _starCounter;
    int count;
    string winstars;

    //chatgpt
    //public Text starCountText; // Reference to the UI text element to display the star count

    private int collectedStars = 0; // Number of stars collected by the player
    private int totalStarsInLevel; // Total number of stars in the current level (calculated automatically)

    // Call this method to calculate the total number of stars in the level
    private void CalculateTotalStarsInLevel()
    {
        // Retrieve all the star objects in the level and count them
        GameObject[] starObjects = GameObject.FindGameObjectsWithTag("Star");
        totalStarsInLevel = starObjects.Length;
        //Debug.Log(totalStarsInLevel);
    }

    // Call this method whenever the player collects a star
    public void CollectStar()
    {
        collectedStars++;
        if(!leaderboardManager.Instance.checkIfLevelDone()){
            leaderboardManager.Instance.updateStartCount();
        }
        if(collectedStars == totalStarsInLevel)
        {
            leaderboardManager.Instance.setCollectedLevel();
        }
        UpdateStarCountUI();
        //hit.collider.gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }

    public string winStar(){
        return winstars =  collectedStars + " out of " + totalStarsInLevel;
    }

    // Call this method to update the UI with the current star count
    private void UpdateStarCountUI()
    {
        _starCounter.text = "Stars:" +collectedStars + "/" + totalStarsInLevel;
    }

    // Start is called before the first frame update
    private void Start()
    {
        CalculateTotalStarsInLevel();
        UpdateStarCountUI();
    }

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _starCounter = _doc.rootVisualElement.Q<Label>("stars");
    }

    
   
}
