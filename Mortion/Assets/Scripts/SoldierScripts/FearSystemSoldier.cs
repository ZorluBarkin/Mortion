using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSystemSoldier : MonoBehaviour
{

    [SerializeField] private float fear;

    public bool playerInSightRange, playerInAttackRange;

    //make these two dynamic ie get the value from SoldierScript
    public float sightRange = 40;

    public float attackRange = 20;

    public LayerMask whatIsGround, whatIsPlayer;

    //outlines
    private Outline outline;
    // used to make outline transparent
    [SerializeField] private Color transparent;

    // Start is called before the first frame update
    void Awake()
    {
        fear = 1.0f;

        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;

        //sightRange = SoldierScript.GetSightRange(); 
        // deos not work, non-static error but making those value static breaks soldier
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if player is in sight range fear grows relatively slowly
        if (playerInSightRange && !playerInAttackRange)
        {
            if (fear > 75)
            {
                fear -= 0.30f;
            }

            fear += 0.25f * PlayerStats.fearTier;

            outline.OutlineWidth = 5f;
            outline.OutlineColor = Color.yellow;
            
        } // if player is in combat range fear grows relatively fast
        if (playerInAttackRange)
        {
            fear += 0.5f * PlayerStats.fearTier;

            outline.OutlineWidth = 9f;
            outline.OutlineColor = Color.red;
        }
        else // fear ticks down over time
        {
            fear -= 0.1f;
        }

        // make the enemy invivble through walls again
        if(fear < 35)
        {
            outline.OutlineWidth = 1f;
            outline.OutlineColor = transparent;
        }

        // fear cannot be negative
        if (fear < 1)
        {
            fear = 1.0f;
        }

        // maximum fear is 100
        if (fear > 100)
        {
            fear = 100;
        }
    }
}
