using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public delegate void StartSpawnDispatcher(int spawnAmount);
    public static event StartSpawnDispatcher SpawnAmount;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {

                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    [Range(1,100)]
    public int TargetScore = 15;
    [Range(1, 200)]
    public int BronzeScore = 20;
    [Range(1, 200)]
    public int SilverScore = 25;
    [Range(1, 200)]
    public int GoldScore = 30;
    public int Score { get; set; }
    private bool PlayerHasWon = false;
    public bool ChallengeMode { get; set; }

    public int levelScrap;

    private enum Rating {Gold, Silver,Bronze, Unrated };

    private Rating ScoreRating;
    public int ScoreRatingInt { get; set; }

    void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);

    }

    public void Reset()
    {
        SubscribeToDispatchers();
        //Call_SpawnAmount(levelScrap);
        Call_ScoreToBeat(TargetScore);
        Call_SpawnAmount(levelScrap);
    }

    void Start()
    {
        PlayerHasWon = false;
        ScoreRating = Rating.Unrated;
        //levelScrap = 15;
        //TargetScore = 15;
        SubscribeToDispatchers();
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        //Call_SpawnAmount(levelScrap);
        if (sceneName == "ChallengeMode")
        {           
            levelScrap = -1;
            ChallengeMode = true;
        }
        Call_ScoreToBeat(TargetScore);
        Call_SpawnAmount(levelScrap);
    }

    private void CheckPlayerWinLose()
    {
        ScoreRatingInt = (int)Rating.Unrated;
        if (Score >= GoldScore)
        {
            Debug.Log("Gold Score reached");
            ScoreRating = Rating.Gold;
            ScoreRatingInt = (int)ScoreRating;
            Debug.Log(ScoreRatingInt);
            PlayerHasWon = true;
            //Call_PlayerWon(true);
        }
        else if (Score < GoldScore && Score >= SilverScore)
        {
            Debug.Log("Silver Score reached");
            ScoreRating = Rating.Silver;
            ScoreRatingInt = (int)ScoreRating;
            Debug.Log(ScoreRatingInt);
            PlayerHasWon = true;
            //Call_PlayerWon(true);
        }
        else if (Score < SilverScore && Score >= BronzeScore)
        {
            Debug.Log("Bronze Score reached");
            ScoreRating = Rating.Bronze;
            ScoreRatingInt = (int)ScoreRating;
            Debug.Log(ScoreRatingInt);
            PlayerHasWon = true;
            //Call_PlayerWon(true);
        }
        else if (Score >= TargetScore)
        {
            Debug.Log("TargetScore reached");
            PlayerHasWon = true;
        }
        else if(Score < TargetScore)
        {
            PlayerHasWon = false;
            Call_GameOver(true);
        }

        if (PlayerHasWon == true)
        {
            Call_PlayerWon(true);
        }
        

    }

    public delegate void GameEventDispatcher(bool gameOver);
    public static event GameEventDispatcher GameOver;

    public void Call_GameOver(bool gameOver)
    {
        Debug.Log("Game over condition met");
        GameOver(gameOver);
    }

    public delegate void PlayerWinsEventDispatcher(bool playerWins);
    public static event PlayerWinsEventDispatcher PlayerWins;

    public void Call_PlayerWon(bool playerWins)
    {
        if (PlayerWins != null)
        {
            PlayerWins(playerWins);
        }
    }

    public delegate void ScoreToBeatDispatcher(int scoreToBeat);
    public static event ScoreToBeatDispatcher ScoreToBeat;

    public void Call_ScoreToBeat(int scoreToBeat)
    {
        if (ScoreToBeat != null)
        {            
            ScoreToBeat(scoreToBeat);
        }
    }

    //subscribed dispatchers

    void SubscribeToDispatchers()
    {
        Player.Death += OnPlayerDead;
        Player.UpdateScore += OnPlayerScore;
        LevelSpawnerFromPool.SpawnLimit += OnSpawnLimit;
       
        //Call_SpawnAmount(levelScrap);
    }
    void OnPlayerDead()
    {              
        Call_GameOver(true);
        Call_StopSpawning();
    }

    void OnPlayerScore(int score)
    {
        Score = score;
        //if (Score >= TargetScore && gameStillRunning)
        //{
        //    StopAllCoroutines();
        //    gameStillRunning = false;
        //    Call_PlayerWon(true);
        //}
        //if (scrapLeft == 0 && gameStillRunning)
        //{
        //    StopAllCoroutines();
        //    gameStillRunning = false;
        //    Call_GameOver(true);
        //}
    }

    void OnSpawnLimit()
    {
        CheckPlayerWinLose();
    }
    
    public static void Call_SpawnAmount(int spawnAmount)
    {
        if (SpawnAmount != null)
        {
            SpawnAmount(spawnAmount);
        }
    }
    
    public delegate void GameOverSpawn();
    public static event GameOverSpawn GameOverStopSpawning;

    public void Call_StopSpawning()
    {
        if (GameOverStopSpawning != null)
        {
            GameOverStopSpawning();
        }
    }

    private void OnDestroy()
    {
        Player.Death -= OnPlayerDead;
        Player.UpdateScore -= OnPlayerScore;
        LevelSpawnerFromPool.SpawnLimit -= OnSpawnLimit;
    }
}