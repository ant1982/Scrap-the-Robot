using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour {

    public static PowerUpPool current;
    public GameObject PowerUp;
    public GameObject[] PowerUpArray;
    public int poolSize = 10;
    public bool willGrow;    
    private int RandomValue;

    List<GameObject> PowerUpList;

    private void Awake()
    {
        current = this;
        PowerUpArray = Resources.LoadAll<GameObject>("PowerUpPrefabs") as GameObject[];
        CreatePool();
        
    }

     void CreatePool()
    {
        PowerUpList = new List<GameObject>();

        if (PowerUpList != null)
        {
            for (int i = 0; i < poolSize; i++)
            {
                RandomValue = Random.Range(0, PowerUpArray.Length);
                GameObject go = Instantiate(PowerUpArray[RandomValue]);
                if (go != null)
                {
                    AddToPool(go);
                }
            }
        }
    }

    public void AddToPool(GameObject go)
    {
        PowerUpList.Add(go);
        go.SetActive(false);
    }


    public GameObject returnObject()
    {
        if (PowerUpList.Count > 0)
        {
            GameObject obj = PowerUpList[0];
            //projectileList.RemoveAt(0);
            PowerUpList.Remove(obj);
            return obj;
        }
        if (willGrow)
        {
            GameObject o = (GameObject)Instantiate(PowerUp);
            return o;
        }
        return null;
    }

    public void DestroyObjectPool(GameObject obj)
    {
        PowerUpList.Add(obj);
        obj.SetActive(false);
    }

}
