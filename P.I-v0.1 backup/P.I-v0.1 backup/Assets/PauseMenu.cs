using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject restartUI;
    public GameObject inGameOptionsUI;
    GameObject player;
    PlayerHealth playerHealth;
    bool inOptions;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
       

    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape) && !inOptions)
        {
            if (GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
        if (playerHealth.currentHealth <= 0)
        {
            Invoke("ShowRestartUI", 1.5f);
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && inOptions)
        {
            inGameOptionsUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            inOptions = false;
        }


    }
    void ShowRestartUI()
    {
        restartUI.SetActive(true);


    }

   public void Resume()
    {
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;       
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void InGameOptions()
    {
        inGameOptionsUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        inOptions = true;
    }

    public void RestartLevel()
    {
       
        SceneManager.LoadScene("Jogo");
        Time.timeScale = 1f;
    }



   
}
