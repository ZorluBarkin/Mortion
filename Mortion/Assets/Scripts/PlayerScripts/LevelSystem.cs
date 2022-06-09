using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{

    public static float totalXP = 0f;

    public int playerLevel = 0;

    [SerializeField] private float currentlevelXP = 500f;

    // this value is the currency for improving 1 for each improvement of a skill
    public static int skillPoints = 0;

    public RectTransform levelMenu;

    private bool open = false;

    private void Start()
    {
        levelMenu.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // for every level up 1 point is given to the player to use on skills
        if(totalXP >= currentlevelXP)
        {
            skillPoints++;
            playerLevel++;

            // make levelXp have only remainder
            totalXP = totalXP - currentlevelXP;

            // recalculate the new level XP
            LevelCalculator();
        }

        if (Input.GetKeyDown(KeyCode.F1) && open == false)
        {
            Time.timeScale = 0.2f;

            Cursor.lockState = CursorLockMode.None;

            levelMenu.localScale = Vector3.one;

            open = true;

        }
        else if(Input.GetKeyDown(KeyCode.F1) && open == true)
        {
            levelMenu.localScale = Vector3.zero;

            open = false;

            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }

    }

    //calculates the amount of XP needed to pass the new level
    private void LevelCalculator()
    {
        currentlevelXP *= 1.5f;
    }
    
}
