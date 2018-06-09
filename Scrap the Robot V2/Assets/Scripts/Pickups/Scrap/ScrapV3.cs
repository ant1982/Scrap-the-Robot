using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapV3 : BaseScrapClass {

    // Use this for initialization
    void Start()
    {
        ScoreValue = 3;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
