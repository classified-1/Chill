using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float roamRadius = 10f;
    public float attackRadius = 3f;
    public float attackDistance = 1.5f; // Distance at which the monster can attack the player.
    public float playerSpeedIncreaseFactor = 2f;
    public float increasedMonsterSpeed = 2.2f;
    public float normalMonsterSpeed = 2f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float currentRoamTime = 0f;
    private float roamDuration = 5f;

    private static readonly int AttackBool = Animator.StringToHash("IsAttacking");

    private enum MonsterState
    {
        Roaming,
        Chasing,
        Attacking
    }

    private MonsterState currentState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentState = MonsterState.Roaming;
        UpdateRoamingDestination();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case MonsterState.Roaming:
                if (distanceToPlayer < attackRadius)
                {
                    currentState = MonsterState.Chasing;
                }
                else if (currentRoamTime >= roamDuration)
                {
                    UpdateRoamingDestination();
                }
                else
                {
                    currentRoamTime += Time.deltaTime;
                }
                break;

            case MonsterState.Chasing:
                navMeshAgent.SetDestination(player.position);

                // Increase monster speed when player is in attack radius.
                if (distanceToPlayer <= attackRadius)
                {
                    navMeshAgent.speed = increasedMonsterSpeed;
                }
                else
                {
                    navMeshAgent.speed = normalMonsterSpeed;
                }

                if (distanceToPlayer <= attackDistance)
                {
                    currentState = MonsterState.Attacking;
                    animator.SetBool(AttackBool, true); // Start playing the attack animation.
                }
                else if (distanceToPlayer > roamRadius)
                {
                    currentState = MonsterState.Roaming;
                    currentRoamTime = 0f;
                }
                break;

            case MonsterState.Attacking:
                // Make the monster look at the player.
                transform.LookAt(player.position);

                navMeshAgent.SetDestination(transform.position);

                // Delay transition back to chasing until the attack animation is complete.
                if (!animator.GetBool(AttackBool))
                {
                    currentState = MonsterState.Chasing;
                }
                break;
        }

        // If the player moves out of the attack Distance, stop attacking and return to roaming.
        if (currentState == MonsterState.Attacking && distanceToPlayer > attackDistance)
        {
            currentState = MonsterState.Roaming;
            animator.SetBool(AttackBool, false); // Stop playing the attack animation.
        }

        UpdateAnimator();
    }

    private void UpdateRoamingDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
        Vector3 finalPosition = hit.position;

        navMeshAgent.SetDestination(finalPosition);
        currentRoamTime = 0f;
    }

    private void UpdateAnimator()
    {
        float moveSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", moveSpeed);
    }
}
