using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public Transform Player;
    public LayerMask GroundMask, PlayerMask;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    //Attacking
    public float AttackInterval;
    bool AlreadyAttacked;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    void Awake() 
    {
        Player = GameObject.Find("PlayerObj").transform;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerMask);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerMask);
    
        if (!PlayerInSightRange && !PlayerInAttackRange) Patrol();
        if (PlayerInSightRange && !PlayerInAttackRange) Chase();
        if (PlayerInSightRange && PlayerInAttackRange) Attack();
    }

    void Patrol()
    {
        if (!WalkPointSet) SearchWalkPoint();

        if (WalkPointSet) NavAgent.SetDestination(WalkPoint);

        Vector3 DistanceToWalkPoint = transform.position - WalkPoint;
        if (DistanceToWalkPoint.magnitude < 1f) WalkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
    
        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
    
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, GroundMask)) {
            WalkPointSet = true;
        }
    }

    void Chase()
    {
        NavAgent.SetDestination(Player.position);
    }

    void Attack()
    {
        NavAgent.SetDestination(transform.position);
        transform.LookAt(Player);
        if (!AlreadyAttacked) 
        {
            Debug.Log("Attacking!!!");

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), AttackInterval);
        }
    }

    void ResetAttack()
    {
        AlreadyAttacked = false;
    }
}
