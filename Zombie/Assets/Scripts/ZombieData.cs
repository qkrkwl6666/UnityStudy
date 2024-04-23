using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/ZombieDate", fileName = "ZombieData")]
public class ZombieData : ScriptableObject
{
    public float percentage = 1f;
    public float health = 100f;
    public float damage = 20f;
    public float speed = 2f;
    public Color skinColor = Color.white;
}
