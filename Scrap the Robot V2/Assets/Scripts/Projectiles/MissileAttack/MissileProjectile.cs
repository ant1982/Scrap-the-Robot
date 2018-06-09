using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : BaseProjectile
{

    private GameObject player = null;    
    private float speed = 4.0f;
    private float rotateSpeed = 400.0f;
    private bool targetFound;

    Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        DamageValue = 40;
        Invoke("DestroyMissile", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //Raycasting();
        MissileFired();
    }

    void Raycasting()
    {
        //Debug.DrawLine(transform.position, player.transform.position, Color.green);
        //targetFound = Physics2D.Linecast(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player"));
        //if (targetFound == true)
        //{
        //    MissileFired();
        //}
    }

    void MissileFired()
    {
        //float rate = speed * Time.deltaTime;

        Vector2 length = transform.position - player.transform.position;

        length.Normalize();

        float value = Vector3.Cross(length, transform.right).z;

        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, rate);

        rb.angularVelocity = rotateSpeed * value;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damagePlayer = other.gameObject.GetComponent<Player>();
        if (damagePlayer != null)
        {
            damagePlayer.Damage(10);
            Debug.Log("hit player");
            DestroyMissile();
        }

        IBlock HitBlock = other.gameObject.GetComponent<BlockScript>();
        if (HitBlock != null)
        {
            if (HitBlock.HitObstacle())
            {
                Destroy(other.gameObject);
                DestroyMissile();
            }
        }
    }

    private void DestroyMissile()
    {
        Destroy(gameObject);
    }
}
