using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeUI : MonoBehaviour {

    public static ChallengeUI instance;

    public Text playerFinalScore;    
    public int Finalscore;
    
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

    }

    void Start()
    {
        Finalscore = GameManager.Instance.Score;
        playerFinalScore.text = "Your Score was: " + Finalscore;        
        Time.timeScale = 0;
    }
}

