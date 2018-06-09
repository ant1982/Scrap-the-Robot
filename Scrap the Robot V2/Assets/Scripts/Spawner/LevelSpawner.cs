using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {

    bool scrapLimit = true;
    private float spawnHeight = 5.0f;
    private int scrapSpawnAmount;
    private int RandomValue;
    public GameObject[] ScrapArray;

    void Awake()
    {
        GameManager.SpawnAmount += OnSpawnAmount;
    }

    // Use this for initialization
    void Start () {

        ScrapArray = Resources.LoadAll<GameObject>("ScrapPrefabs") as GameObject[];
       
        //scrapSpawnAmount = GameManager.Instance.levelScrap;
        StartCoroutine(Spawn());
    }

    public void OnSpawnAmount(int levelScrap)
    {
        scrapSpawnAmount = levelScrap;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

        while (scrapLimit)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-3.0f, 3.0f),
                    spawnHeight,
                    0.0f
                    );
            Quaternion spawnRotation = Quaternion.identity;
            RandomValue = Random.Range(0, ScrapArray.Length);
            Instantiate(ScrapArray[RandomValue], spawnPosition, spawnRotation);
            scrapSpawnAmount -= 1;
            Debug.Log(scrapSpawnAmount);
            if (scrapSpawnAmount == 0)
            {
                StopSpawning();                         
            }
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }

    }

    private void StopSpawning()
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
}
