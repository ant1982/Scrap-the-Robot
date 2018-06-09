using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUImanager : MonoBehaviour {

    void Start()
    {
        Screen.SetResolution(480, 640, false);
    }

    public void BeginFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadChallengeMode()
    {
        SceneManager.LoadScene("ChallengeMode");
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Debug.Log("Active Scene is: " + scene.name);
        UnpauseGame();

    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        UnpauseGame();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //GameManager.Instance.Reset();
        UnpauseGame();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
