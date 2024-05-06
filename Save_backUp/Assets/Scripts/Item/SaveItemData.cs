using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class SaveItemData
{
    public int instanceId;
    public ItemData data;

    public int customOrder;
    public System.DateTime creationTime;

    public SaveItemData()
    {
        creationTime = System.DateTime.Now;
        instanceId = Animator.StringToHash(creationTime.Ticks.ToString());
    }
}
