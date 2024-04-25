using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureInfo : MonoBehaviour
{
    protected int hp;
    protected int damage;
    protected float speed;
    protected int minHp;

    protected bool isDead = false;
    public abstract void OnDamege(int damage, Vector3 hitPosition);
    public abstract void Die();

}
