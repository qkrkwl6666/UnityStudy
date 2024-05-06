using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikkeData : ScriptableObject
{
    public string NikkeName {  get; set; }
    public string Squad {  get; set; }
    public int Level {  get; set; }
    public int TotalAttack {  get; set; }
    public int Hp {  get; set; }
    public int Damage {  get; set; }
    public int Defence {  get; set; }
}
