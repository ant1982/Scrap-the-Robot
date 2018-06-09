using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    public Slider healthBar;

    // Use this for initialization

    void OnEnable()
    {
        Player.CurrentHealth += HealthReceived;
    }

    private void HealthReceived(int healthAmount)
    {
        healthBar.value = healthAmount;
    }
}
