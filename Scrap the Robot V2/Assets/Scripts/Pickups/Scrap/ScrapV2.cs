using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapV2 : BaseScrapClass {

    void Start()
    {
        ScoreValue = 2;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
