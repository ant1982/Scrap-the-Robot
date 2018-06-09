using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreShield : BasePowerUp {

	// Use this for initialization
	void Start () {

        PowerUpIndex = 0;
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

}
