using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseProjectile {

    protected float speed = 10.0f;
    public Vector3 Direction = Vector3.up;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IStun stunEnemy = other.gameObject.GetComponent<Enemy>();
        if(stunEnemy != null)
        {
            stunEnemy.StunAttack();
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

}
