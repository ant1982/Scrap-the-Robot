using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawnerFromPool : MonoBehaviour
{

    public static LevelSpawnerFromPool instance;

    bool CanSpawn = true;
    private float Height = 5.0f;
    private int SpawnAmount;    
    public GameObject[] ScrapArray;      

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

        //Player.Death += OnPlayerDead;

    }
    // Use this for initialization
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if (sceneName == "Main Menu")
        {
            CanSpawn = false;
        }
        GameManager.SpawnAmount += OnSpawnAmount;
        GameManager.GameOverStopSpawning += OnStopSpawning;


    }

    public void OnSpawnAmount(int levelScrap)
    {        
        SpawnAmount = levelScrap;
        if (CanSpawn)
        {
            StartCoroutine(SpawnFromPool());
        }
    }

    IEnumerator SpawnFromPool()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-3.0f, 3.0f),
                    Height,
                    0.0f
                    );
            Quaternion spawnRotation = Quaternion.identity;
            //RandomValue = Random.Range(0, ScrapArray.Length);
            //Instantiate(ScrapArray[RandomValue], spawnPosition, spawnRotation);

            GameObject obj = ScrapPool.current.returnObject();

            if (obj == null) yield return null;

            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;

            obj.SetActive(true);

            SpawnAmount -= 1;            
            Call_ScrapLeft(SpawnAmount);
            if (SpawnAmount == 0)
            {
                yield return new WaitForSeconds(3.0f);
                StopSpawn();                
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }

    }

    private void StopSpawn()
    {
        StopAllCoroutines();
        Call_Spawnlimit();
    }

    public delegate void NoSpawnLeftDispatcher();
    public static event NoSpawnLeftDispatcher SpawnLimit;

    public void Call_Spawnlimit()
    {
        SpawnLimit();
    }

    public delegate void CurrentSpawnAmount(int scrapLeft);
    public static event CurrentSpawnAmount ScrapLeft;

    public void Call_ScrapLeft(int scrapLeft)
    {
        if (ScrapLeft != null)
        {
            ScrapLeft(scrapLeft);
        }
    }

    //public void OnPlayerDead()
    //{
    //    StopAllCoroutines();
    //}

    public void OnStopSpawning()
    {
        StopScrapSpawning();
    }

    private void StopScrapSpawning()
    {
        StopAllCoroutines();
    }

}
