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
    private int score;
    private float attackSpeed;
    private float attackTime;
    private PlayerInfo playerInfo;

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

        //GameObject.FindWithTag("GameManager").GetComponentInChildren<EnemysSpawner>().disableEnemies.Remove(this.gameObject);
        EnemysSpawner.disableEnemies.Remove(this.gameObject);
    }

    private void OnEnable()
    {
        hp = startHp;
        navMeshAgent.enabled = true;
        StartCoroutine(NavMove());
    }

    private void Awake()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        playerInfo = playerPosition.gameObject.GetComponent<PlayerInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public override void OnDamege(int damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (isDead) return;

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
        score = enemyData.score;
        attackSpeed = enemyData.attackSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        attackTime = Time.time;
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

        if(Vector3.Distance(playerPosition.position, transform.position) < 1.5 &&
            Time.time > attackTime)
        {
            playerInfo.OnDamege(damage, Vector3.zero, Vector3.zero);
            attackTime = Time.time + attackSpeed;
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
