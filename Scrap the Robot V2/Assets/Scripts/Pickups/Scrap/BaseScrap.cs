using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScrapClass : MonoBehaviour
{    
    protected int ScoreValue;
    public ScrapPool pool;
    public GameObject player;
    Vector3 target;
    Rigidbody2D rb;
    private float speed = 4.0f;
    bool MagnetOn;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        ICollectitem playerCollectItem = other.gameObject.GetComponent<Player>();
        //IMagnetShield magnetShield = other.gameObject.GetComponent<Player>();
        if (playerCollectItem != null)
        {
            playerCollectItem.CollectItem(ScoreValue);
            ReturnScrap();
        }
        //if (magnetShield != null)
        //{            
        //    //magnetShield.MagnetShieldActive();
        //    if (magnetShield.MagnetShieldActive())
        //    {
        //        //rb.gravityScale = -1.0f;                
        //        Vector3 targetPosition = magnetShield.ReturnPosition();                
        //        homing = true;
        //        float rate = speed * Time.deltaTime;

        //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, rate);
        //    }
        //}
    }

    void Awake()
    {
        pool = GameObject.Find("ScrapObjectPool").GetComponent<ScrapPool>();
        rb = GetComponent<Rigidbody2D>();       
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player");
        Invoke("ReturnScrap", 3f);
        IMagnetShield magnetShield = player.gameObject.GetComponent<Player>();
        if (magnetShield != null)
        {
            //magnetShield.MagnetShieldActive();
                if (magnetShield.MagnetShieldActive())
                {
                MagnetOn = true;
                Vector3 target = magnetShield.ReturnPosition();
                MagneticEffect();
                }
            else
            {
                MagnetOn = false;
            }
        }
    }

    void Update()
    {
        if (MagnetOn)
        {
            MagneticEffect();
        }
        rb.gravityScale = 1.0f;
    }

    protected void MagneticEffect()
    {
        rb.gravityScale = 0.0f;
        Vector3 playerPosition = target;
        float rate = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, rate);
    }

    private void ReturnScrap()
    {
        gameObject.SetActive(false);
        pool.AddToPool(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
