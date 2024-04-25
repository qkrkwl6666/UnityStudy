using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CreatureInfo
{
    private Animator animator;

    protected override void OnDamege(float damage, Vector3 hitPosition)
    {
        hp -= damage;

        if(hp < 0 && !isDead)
        {
            Die();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Die()
    {
        isDead = true;
        hp = 0;
        animator.SetTrigger("Death");
    }
}
