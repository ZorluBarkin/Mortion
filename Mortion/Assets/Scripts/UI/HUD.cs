using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // ammo is calculated and displayed over gun script

    public Text healthText;

    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = HealthToString();
    }
    
    private string HealthToString()
    {
        // casted to int because we want to see whole numbers not floating points
        int healthInt = (int) PlayerStats.Health;
        string health = healthInt.ToString();
        return ("Health: " + health);
    }
}
