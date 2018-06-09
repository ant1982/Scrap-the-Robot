using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour {

    //this is an abstract class for power ups

    protected int PowerUpIndex;
    public PowerUpPool pool;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IPowerUp playerCollectPowerUp = other.gameObject.GetComponent<Player>();
        if (playerCollectPowerUp != null)
        {
            playerCollectPowerUp.PowerUpCollected(PowerUpIndex);
            ReturnPowerUp();
        }
    }

    void Awake()
    {
        pool = GameObject.Find("PowerUpPool").GetComponent<PowerUpPool>(); 
    }

    private void OnEnable()
    {
        Invoke("ReturnPowerUp", 3f);
    }

    protected virtual void ReturnPowerUp()
    {
        gameObject.SetActive(false);
        pool.AddToPool(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
