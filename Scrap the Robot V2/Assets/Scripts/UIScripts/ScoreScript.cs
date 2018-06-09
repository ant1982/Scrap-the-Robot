using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text ScrapLeft;
    public Text score;
    private int currentScore;

    // Use this for initialization
    void Start()
    {
        LevelSpawnerFromPool.ScrapLeft += OnScrapLeft;
        Player.UpdateScore += OnScoreUpdate;
        if (score == null)
        {
            score = GameObject.Find("Score").GetComponent<Text>();
        }
    }
    
    public void OnScoreUpdate(int scoreUpdate)
    {
        if (score == null)
        {
            score = GameObject.Find("Score").GetComponent<Text>();
        }
        currentScore = scoreUpdate;
        score.text = "Score: " + currentScore;
    }

    public void OnScrapLeft(int scrap)
    {
        if (GameManager.Instance.ChallengeMode == false)
        {
            if (ScrapLeft == null)
            {
                ScrapLeft = GameObject.Find("ScrapLeft").GetComponent<Text>();

            }
            ScrapLeft.text = "Scrap left: " + scrap;
        }
    }
}
