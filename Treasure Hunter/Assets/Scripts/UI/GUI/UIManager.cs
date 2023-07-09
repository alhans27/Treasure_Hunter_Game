using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    
    //buat tombol Play di StartMenu
    public void StartGame()
    {
        SceneManager.LoadScene("LevelHasna");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // buat tombol Level di StartMenu
    public void GoToLevelSelection()
    {
        SceneManager.LoadScene("PilihanLevel");
    }

    // buat tombol Credits di StartMenu
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    // buat tombol Pause di Layar Level
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    //buat tombol segitiga Play di panel pause game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    //buat tombol bentuk Home
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // buat tombol quit yg ada dimana aja
    public void Quit()
    {
        Application.Quit();
    }
}
