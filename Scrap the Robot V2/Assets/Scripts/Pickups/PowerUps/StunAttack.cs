using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAttack : BasePowerUp {

	// Use this for initialization
	void Start () {
        PowerUpIndex = 3;
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
    } 
}
