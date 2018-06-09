using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool current;
    public GameObject projectile;
    public int poolSize = 20;
    public bool willGrow;
    List<GameObject> projectileList;

    private void Awake()
    {
        current = this;
        CreatePool();
    }

    void Start()
    {
        //CreatePool();
    }

    void CreatePool()
    {
        projectileList = new List<GameObject>();

        if (projectile != null)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(projectile);
                if (go != null)
                {
                    addToPool(go);
                }
            }
        }
    }

    public void addToPool(GameObject go)
    {
        projectileList.Add(go);
        go.SetActive(false);


    }

    public GameObject returnObject()
    {
        if (projectileList.Count > 0)
        {
            GameObject obj = projectileList[0];
            //projectileList.RemoveAt(0);
            projectileList.Remove(obj);
            return obj;
        }
        if (willGrow)
        {
            GameObject o = (GameObject)Instantiate(projectile);
            return o;
        }
        return null;
    }

    public void DestroyObjectPool(GameObject obj)
    {
        projectileList.Add(obj);
        obj.SetActive(false);
    }

}
