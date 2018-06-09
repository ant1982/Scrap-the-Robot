using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack1 : BaseProjectile {

    public ProjectilePool pool;
    public Vector3 Direction = Vector3.down;
    protected float speed = 5.0f;    

    void Awake()
    {
        pool = GameObject.Find("ObjectPoolActor").GetComponent<ProjectilePool>();
    }

    void Start()
    {
        DamageValue = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IBlock HitBlock = other.gameObject.GetComponent<BlockScript>();
        if (HitBlock != null)
        {
            if (HitBlock.HitObstacle())
            {
                DestroyProjectile();
            }
        }

        IDamageable damagePlayer = other.gameObject.GetComponent<Player>();
        if (damagePlayer != null)
        {
            damagePlayer.Damage(10);            
            DestroyProjectile();
        }
    }

    private void OnEnable()
    {
        Invoke("DestroyProjectile", 2f);
    }

    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
        //ProjectilePool.current.addToPool(gameObject);
        pool.addToPool(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
