using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CreatureInfo
{
    public ParticleSystem hitParticle;
    private Transform playerPosition;
    private int startHp;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int score = 20;

    public override void Die()
    {
        isDead = true;
        hp = 0;
        animator.SetTrigger("Death");

        if(UIManager.instance != null ) 
            UIManager.instance.UpdateScore(score);

        StartCoroutine(EnemyActive(false, 5f));
    }

    IEnumerator EnemyActive(bool active, float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(active);

        EnemysSpawner.disableEnemies.Remove(this.gameObject);
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

    public override void OnDamege(int damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        hp -= damage;

        hitParticle.transform.forward = hitNormal;
        hitParticle.Play();

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
        gameObject.SetActive(false);
        // hp = 100;
        // StartCoroutine(NavMove());
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

    public void StartSinking()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }


    IEnumerator NavMove()
    {
        while (true)
        {
            if (!playerPosition || !navMeshAgent.isOnNavMesh || !navMeshAgent.enabled) yield break;

            navMeshAgent.SetDestination(playerPosition.position);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
