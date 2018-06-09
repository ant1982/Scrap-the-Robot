using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpSpawner : MonoBehaviour {

    public static PowerUpSpawner instance;

    
    private float Height = 5.0f;
    private int SpawnAmount;
    private int RandomValue;

    void Awake()
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
        // Use this for initialization
    void Start()
    {       
        
    StartCoroutine(SpawnFromPool());
    GameManager.GameOver += OnGameOver;
    GameManager.PlayerWins += OnPlayerWins;
        
    }

    IEnumerator SpawnFromPool()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));

        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-3.0f, 3.0f),
                    Height,
                    0.0f
                    );
            Quaternion spawnRotation = Quaternion.identity;
            //RandomValue = Random.Range(0, ScrapArray.Length);
            //Instantiate(ScrapArray[RandomValue], spawnPosition, spawnRotation);

            GameObject obj = PowerUpPool.current.returnObject();

            if (obj == null) yield return null;

            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;

            obj.SetActive(true);
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }

    }

    void OnGameOver(bool gameOver)
    {
        //StopAllCoroutines();
    }

    void OnPlayerWins(bool playerWins)
    {
        //StopAllCoroutines();
    }
}
