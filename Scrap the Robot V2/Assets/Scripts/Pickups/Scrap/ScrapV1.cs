using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapV1 : BaseScrapClass {

    void Start()
    {
        ScoreValue = 1;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

}
