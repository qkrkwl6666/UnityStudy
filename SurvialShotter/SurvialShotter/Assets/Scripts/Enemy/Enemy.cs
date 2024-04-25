using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CreatureInfo
{
    private Transform playerPosition;
    private int startHp;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    public override void Die()
    {
        isDead = true;
        hp = 0;
        animator.SetTrigger("Death");

        navMeshAgent.enabled = false;
        
    }

    private void OnEnable()
    {
        navMeshAgent.enabled = true;
        StartCoroutine(NavMove());
    }

    private void Awake()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public override void OnDamege(int damage, Vector3 hitPosition)
    {
        hp -= damage;

        Debug.Log("Enemy Hit");

        if (hp < 0 && !isDead)
        {
            Die();
        }
    }

    public void SetUp(EnemyData enemyData)
    {
        startHp = enemyData.startHp;
        hp = startHp;
        damage = enemyData.damage;
        speed = enemyData.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NavMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
 
    IEnumerator NavMove()
    {
        while (true)
        {
            if (!playerPosition && !navMeshAgent.enabled) yield break;

            navMeshAgent.SetDestination(playerPosition.position);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
