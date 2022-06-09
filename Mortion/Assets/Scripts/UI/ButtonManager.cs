using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // assign every button in editor

    // Passive Skill Buttons
    public Button movementSkill;
    public Button healthButton;
    public Button dashButton;
    public Button fearButton;


    // Combat Skill Buttons
    public Button defensiveWall;
    //public Button longRangeWeapon;
    //public Button shotgun
    //public Button explosiveDevice
    public Button ammoButton;

    // Texts
    public Text skillPointCounter;
    public Text declineText;
    public Text levelSummary;

    // levels
    private int movementLevel = 1;
    private int healthLevel = 1;
    private int dashLevel = 1;
    private int fearTier = 1;
    private int defensiveWallLevel = 1;
    private int longRangeGunLevel = 0;
    private int shotgunLevel = 0;
    private int explosiveLevel = 0;
    private int ammoTier = 1;

    // every thing except unlockables are implemented

    // Start is called before the first frame update
    void Start()
    {

        //passive Buttons
        movementSkill.onClick.AddListener(MovementUpgrade);
        healthButton.onClick.AddListener(HealthUpgrade);
        dashButton.onClick.AddListener(DashUpgrade);
        fearButton.onClick.AddListener(FearUpgrade);

        //combat buttons
        defensiveWall.onClick.AddListener(DefensiveWallUpgrade);
        //longRangeWeapon.onClick.AddListener();
        //shotgun.onClick.AddListener();
        //explosiveDevice.onClick.AddListener();
        ammoButton.onClick.AddListener(AmmoUpgrade);

        // skill points to string
        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();

    }

    private void MovementUpgrade()
    {
        if (LevelSystem.skillPoints > 0)
        { // adds 0.25 every time it levels up because 1.25 * 8(standart player speed) = 10
            PlayerStats.movementTier += 0.25f;
            movementLevel++;
            LevelSystem.skillPoints--;
        }
        else
        {
            declineText.text = NotEnough();
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    private void HealthUpgrade()
    {
        if (LevelSystem.skillPoints > 0)
        {
            PlayerStats.Health += 50;
            healthLevel++;
            LevelSystem.skillPoints--;
        }
        else
        {
            declineText.text = NotEnough();
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    private void DashUpgrade()
    {
        if (LevelSystem.skillPoints > 0 && dashLevel < 4)
        {
            PlayerStats.dashTier -= 0.25f;
            dashLevel ++;
            LevelSystem.skillPoints--;
        }
        else if(LevelSystem.skillPoints == 0)
        {
            declineText.text = NotEnough();
        }else if(dashLevel == 4)
        {
            declineText.text = " Max Level Reached.";
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    private void FearUpgrade()
    {
        if (LevelSystem.skillPoints > 0)
        {
            PlayerStats.fearTier++;
            fearTier++;
            LevelSystem.skillPoints--;
        }
        else
        {
            declineText.text = NotEnough();
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    private void DefensiveWallUpgrade()
    {

        if (LevelSystem.skillPoints > 0)
        {
            DefensiveWallScript.wallDuration *= 1.5f;
            defensiveWallLevel++;
            LevelSystem.skillPoints--;
        }
        else
        {
            declineText.text = NotEnough();
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    private void AmmoUpgrade()
    {

        if (LevelSystem.skillPoints > 0)
        {
            Gun.ammo += 50;
            Gun.initialAmmo += 50;
            ammoTier++;
            LevelSystem.skillPoints--;
        }
        else
        {
            declineText.text = NotEnough();
        }

        skillPointCounter.text = SkillToString();
        levelSummary.text = LevelSummary();
    }

    // say the remaining skill points
    private string SkillToString()
    {
        string skillPoints = LevelSystem.skillPoints.ToString();
        return ("Skill Points Remaining: " + skillPoints);
    }

    // not enough skill points message to string
    private string NotEnough()
    {
        return ("Not Enough Skill Points");
    }

    private string LevelSummary()
    {
        string result = "Movement Skill: " + movementLevel + "\n";
        result += "Health skill: " + healthLevel + ", Total Health: " + PlayerStats.Health +"\n";
        result += "Dash Skill: " + dashLevel + "\n";
        result += "Fear Tier: " + fearTier + "\n";
        result += "Defensive Wall Level: " + defensiveWallLevel + "\n";
        result += "Long Range Gun Level: " + longRangeGunLevel + "\n";
        result += "Shotgun Level: " + shotgunLevel + "\n";
        result += "Explosive Level: " + explosiveLevel + "\n";
        result += "Ammo Tier: " + ammoTier;

        return result;
    }
}
