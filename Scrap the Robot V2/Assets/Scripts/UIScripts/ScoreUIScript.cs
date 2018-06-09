using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIScript : MonoBehaviour
{
    public Text scoreText;
    public int playerWinScore;

    void Awake()
    {
        GameManager.ScoreToBeat += OnScoreToBeat;
    }

    void OnEnable()
    {

    }

    // Use this for initialization
    void Start()
    {
        playerWinScore = 0;
        //GameManager.ScoreToBeat += OnScoreToBeat;
        scoreText.CrossFadeAlpha(1.0f, 0.01f, false);
        scoreText.CrossFadeAlpha(0.0f, 2.5f, false);
    }

    public void OnScoreToBeat(int scoreToBeat)
    {
        playerWinScore = scoreToBeat;
        if (scoreText != null)
        {
            scoreText = GameObject.Find("TargetScoreText").GetComponent<Text>();
            scoreText.text = "The Score to beat is: " + playerWinScore;

        }
    }
}
