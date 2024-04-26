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
    public bool isDead { get; protected set; } = false;
    public abstract void OnDamege(int damage, Vector3 hitPosition, Vector3 hitNormal);
    public abstract void Die();

}
