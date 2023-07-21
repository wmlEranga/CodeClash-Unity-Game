using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inputUIs;

    public GameObject topUI;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        //inputUIs.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }

            else{
                PauseGame();
            }
        }
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        inputUIs.SetActive(false);
        topUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        //levelSound.instance.MusicSource.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        inputUIs.SetActive(true);
        topUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        //levelSound.instance.MusicSource.Play();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
