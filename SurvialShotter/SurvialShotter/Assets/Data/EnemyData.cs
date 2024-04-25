using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float percentage;
    public float speed;
    public int startHp;
    public int damage;
    public int score;
}
