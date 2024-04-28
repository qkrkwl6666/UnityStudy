using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyData;

public class Enemy : CreatureInfo
{
    public EnemyType enemyType;
    public ParticleSystem hitParticle;
    private Transform playerPosition;
    private int startHp;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int score;
    private float attackSpeed;
    private float attackTime;
    private PlayerInfo playerInfo;
    private float targetDistance = 2f;

    private AudioSource audioSource;

    public AudioClip deathClip;
    public AudioClip hurtClip;
    
    public event Action<Enemy> OnEnemyDie;

    public override void Die()
    {
        isDead = true;
        hp = 0;
        animator.SetTrigger("Death");
        audioSource.PlayOneShot(deathClip);

        if (UIManager.instance != null ) 
            UIManager.instance.UpdateScore(score);

        StartCoroutine(EnemyActive(false, 5f));
    }

    IEnumerator EnemyActive(bool active, float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(active);

        OnEnemyDie?.Invoke(this);
    }

    private void OnEnable()
    {
        isDead = false;
        hp = startHp;
        navMeshAgent.enabled = true;
        GetComponent<Collider>().enabled = true;
        StartCoroutine(NavMove());
    }

    private void Awake()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        playerInfo = playerPosition.gameObject.GetComponent<PlayerInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnDamege(int damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (isDead) return;

        hp -= damage;

        hitParticle.transform.forward = hitNormal;
        hitParticle.Play();
        audioSource.PlayOneShot(hurtClip);

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
        enemyType = enemyData.enemyType;
    }

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMove", playerPosition);

        if(Vector3.Distance(playerPosition.position, transform.position) < targetDistance &&
            Time.time > attackTime && !isDead)
        {
            playerInfo.OnDamege(damage, Vector3.zero, Vector3.zero);
            attackTime = Time.time + attackSpeed;
        }
    }

    public void StartSinking()
    {
        GetComponent<Collider>().enabled = false;
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
