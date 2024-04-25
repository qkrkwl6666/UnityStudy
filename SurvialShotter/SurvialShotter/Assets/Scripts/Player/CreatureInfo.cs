using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureInfo : MonoBehaviour
{
    protected float starthp = 100;
    protected float hp;
    protected float speed = 5f;

    protected bool isDead = false;

    protected abstract void OnDamege(float damage, Vector3 hitPosition);

    private void Awake()
    {
        hp = starthp;
    }

    private void OnEnable()
    {
        hp = starthp;
        isDead = false;
    }


}
