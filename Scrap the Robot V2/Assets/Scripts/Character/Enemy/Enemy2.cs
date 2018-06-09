using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : BaseCharacter {

    public GameObject MissileTurret;
    private GameObject Missile;

    void Awake()
    {
        Missile = Resources.Load("MissilePrefab") as GameObject;
    }

	// Use this for initialization
	void Start () {

        StartCoroutine(MissileAttack());
        //Instantiate(Missile, MissileTurret.transform.position, MissileTurret.transform.rotation);
    }

    IEnumerator MissileAttack()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));

        while (true)
        {
            Instantiate(Missile, MissileTurret.transform.position, MissileTurret.transform.rotation);
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }
    }
}
