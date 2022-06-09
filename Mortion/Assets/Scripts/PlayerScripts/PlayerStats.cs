using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public float XP;
    public static float Health = 100f;
    private float healthLimit;

    // for trigering the health regen
    public static bool gotHit;
    private float cooldown; // set to count up to 3 seconds defined below

    // Tiers
    public static float movementTier =1f;
    public static float dashTier=1f;
    public static int fearTier=1;

    // Unlockables
    public static int longRangeGunTier = 0;
    public static int shotgunTier = 0;
    public static int explosiveTier = 0;

    // Start is called before the first frame update
    void Start()
    {
        fearTier = 1;
        healthLimit = Health;
        cooldown = 0.0f;
        gotHit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        XP = LevelSystem.totalXP;

        if(Health < healthLimit && cooldown >= 0.0f && cooldown <= 3.0f && gotHit == true)
        {

            cooldown += Time.deltaTime;

        }else if (Health < healthLimit && cooldown >= 3.0f)
        {

            Health += 0.1f;

        }else if (Health == healthLimit && cooldown >= 3.0f)
        {
            cooldown = 0.0f;
            gotHit = false;
        }

        if(Health > 100)
        {
            Health = 100;
        }
    }

}
