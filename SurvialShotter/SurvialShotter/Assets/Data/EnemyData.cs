using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    {
        ZomBear = 0,
        Zombunny = 1,
        Hellephant = 2,
        Count = 3,
    }

    public EnemyType enemyType;
    public float percentage;
    public float speed;
    public float attackSpeed;
    public int startHp;
    public int damage;
    public int score;
}
