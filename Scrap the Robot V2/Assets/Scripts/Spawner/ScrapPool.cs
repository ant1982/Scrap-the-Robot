using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPool : MonoBehaviour {

    public static ScrapPool current;
    public GameObject scrap;
    public GameObject[] ScrapArray;
    public int poolSize = 40;
    public bool willGrow;
    private int RandomValue;

    List<GameObject> ScrapList;

    private void Awake()
    {
        current = this;
        ScrapArray = Resources.LoadAll<GameObject>("ScrapPrefabs") as GameObject[];
        CreatePool();
        
    }

    void CreatePool()
    {
        ScrapList = new List<GameObject>();

        if (ScrapList != null)
        {
            for (int i = 0; i < poolSize; i++)
            {
                RandomValue = Random.Range(0, ScrapArray.Length);
                GameObject go = Instantiate(ScrapArray[RandomValue]);
                if (go != null)
                {
                    AddToPool(go);
                }
            }
        }
    }

    public void AddToPool(GameObject go)
    {
        ScrapList.Add(go);
        go.SetActive(false);
    }

    public GameObject returnObject()
    {
        if (ScrapList.Count > 0)
        {
            GameObject obj = ScrapList[0];
            //projectileList.RemoveAt(0);
            ScrapList.Remove(obj);
            return obj;
        }
        if (willGrow)
        {
            GameObject o = (GameObject)Instantiate(scrap);
            return o;
        }
        return null;
    }

    public void DestroyObjectPool(GameObject obj)
    {
        ScrapList.Add(obj);
        obj.SetActive(false);
    }
}
