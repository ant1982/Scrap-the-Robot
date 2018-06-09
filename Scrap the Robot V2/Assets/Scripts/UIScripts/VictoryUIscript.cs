using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUIscript : MonoBehaviour {

    public static VictoryUIscript instance;

    public Text playerFinalScore;
    public Text playerRating;
    public int Finalscore;
    public int Rating;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;            
        }

        //Player.Death += OnPlayerDead;

    }

    // Use this for initialization
    void Start ()
    {
        Finalscore = GameManager.Instance.Score;
        playerFinalScore.text = "Your Score was: " + Finalscore;
        Rating = GameManager.Instance.ScoreRatingInt;
        if(Rating == 0)
        {
            playerRating.text = "Your Rating was Gold!";
        }
        else if(Rating == 1)
        {
            playerRating.text = "Your Rating was Silver!";
        }
        else if (Rating == 2)
        {
            playerRating.text = "Your Rating was Bronze!";
        }  
        else if(Rating  == 3)
        {
            playerRating.text = "Better luck next time";
        }
        Time.timeScale = 0;
    }	
}
