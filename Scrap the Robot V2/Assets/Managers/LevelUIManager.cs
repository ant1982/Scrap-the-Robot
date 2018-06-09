using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

    
    public GameObject GameOverMenu = null;
    public GameObject VictoryMenu = null;
    public GameObject ChallengeMenu = null;

    public static LevelUIManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {     
        GameManager.GameOver += OnGameOver;
        GameManager.PlayerWins += OnPlayerWin;
        GameOverMenu = Resources.Load("GameOver") as GameObject;
        VictoryMenu = Resources.Load("VictoryScreen") as GameObject;
        ChallengeMenu = Resources.Load("ChallengeScreen") as GameObject;
    }

    void OnGameOver(bool gameOver)
    {
        if (gameOver == true && GameManager.Instance.ChallengeMode == false)
        {            
            Instantiate(GameOverMenu, new Vector3(1, 1, 0), Quaternion.identity);
        }
        else if(gameOver == true && GameManager.Instance.ChallengeMode == true)
        {
            Instantiate(ChallengeMenu, new Vector3(1, 1, 0), Quaternion.identity);
            Debug.Log("Challenge mode screen triggered");
        }
    }

    void OnPlayerWin(bool playerWins)
    {        
        Instantiate(VictoryMenu, new Vector3(1, 1, 0), Quaternion.identity);
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= OnGameOver;
        GameManager.PlayerWins -= OnPlayerWin;       
    }
}

