using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWallScript : MonoBehaviour
{

    [SerializeField] private GameObject defensiveWall; //assign in editor

    public static float wallDuration = 2;

    private float duration;

    private bool startCount;

    private float spawnDistance = 5.0f;

    public GameObject player; // assign in editor

    private GameObject wallInstance;



    // Start is called before the first frame update
    void Start()
    {
        startCount = false;  
        duration = wallDuration; // to make sure duration is not 0
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.V) && startCount == false)
        {
            SpawnWall();
            startCount = true;
        }

        if (startCount)
        {
            duration -= Time.deltaTime;

            if(duration <= 0)
            {
                Destroy(wallInstance); // note to myself, destroy the instance not the object // works now yaaaay!
                startCount = false;
                duration = wallDuration;
            }
                
        }

    }

    // spawns defensive wall using instantiate method
    private void SpawnWall()
    {
        // this is for checking the new time after upgrade
        duration = wallDuration;

        // getting the wall in front of the player
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        Quaternion playerRotation = player.transform.rotation;

        wallInstance = Instantiate(defensiveWall, spawnPos, playerRotation);

    }

}
