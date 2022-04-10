
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public float throwForce;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange, minCloseRange;
    public bool playerInSightRange, playerInAttackRange, playerTooClose;

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerTooClose = Physics.CheckSphere(transform.position, minCloseRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && !playerTooClose) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !playerTooClose) AttackPlayer();
        if (playerTooClose) RollBack();
    }

    private void RollBack()
    {
        var distanceBetween = Vector3.Distance(player.position, transform.position);
        var distanceRemaining = minCloseRange - distanceBetween;
        agent.SetDestination(transform.position - new Vector3(distanceRemaining, 0, distanceRemaining));
    }
    
    private void Patroling()
    {
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
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (alreadyAttacked) return;
        var distanceToTarget = Vector3.Distance(player.position, transform.position);
        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        rb.AddForce(transform.up * distanceToTarget / 3, ForceMode.Impulse);

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, player.position);
    }
}
