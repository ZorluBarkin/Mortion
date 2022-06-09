using UnityEngine;
using UnityEngine.AI;

public class SoldierScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 20;

    private Animator soldierAnimator;

    private GameObject Soldier;

    private float bulletSpawnDistance = 1.1f;

    [SerializeField] private AudioSource death;


    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange = 100;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Soldier = GameObject.Find("Soldier");
        agent = GetComponent<NavMeshAgent>();
        soldierAnimator = GetComponent<Animator>();
        //death = GetComponent<AudioSource>();
    }

    // Update
    private void Update()
    {
        if (health <= 0)
        {
            death.Play();
            Destroy(this.transform.parent.gameObject); //destroys the parent
            LevelSystem.totalXP += 100; // gives 100 XP for basic enemy
            //Debug.Log(LevelSystem.totalXP); // used for testing
        }
        else
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

    }

    // Patrolling method
    private void Patroling()
    {
        soldierAnimator.SetBool("run", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        soldierAnimator.SetBool("run", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        System.Console.WriteLine("busm");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        soldierAnimator.SetBool("run", false);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            soldierAnimator.SetBool("shoot", true);

            // getting the bullet to spawn in front of the soldier
            Vector3 soldierPos = this.transform.position;
            soldierPos.y += 2.0f;
            Vector3 soldierDirection = this.transform.forward;
            Quaternion soldierRotation = this.transform.rotation;

            Vector3 bulletSpawnPos = soldierPos + soldierDirection * bulletSpawnDistance;
            GameObject tempBullet = Instantiate(projectile, bulletSpawnPos, soldierRotation);

            tempBullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Rigidbody rb = tempBullet.GetComponent<Rigidbody>();
            BulletScript bscript = tempBullet.GetComponent<BulletScript>();
            bscript.main = false;
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(new Vector3(transform.up.x, transform.up.y - 4f, transform.up.z), ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        soldierAnimator.SetBool("shoot", false);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        LevelSystem.totalXP += 100; //test
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);

    }
    private void DestroyEnemy()
    {
        LevelSystem.totalXP += 100;
        death.PlayOneShot(death.clip);
        System.Console.WriteLine("played");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}