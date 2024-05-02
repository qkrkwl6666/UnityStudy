using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemtableTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var itemTable = DataTableMgr.Get<ItemTable>(DataTablesIds.Item);

        //Debug.Log(itemTable.Get("Item1").GetSprite);
        //Debug.Log(itemTable.Get("Item1").GetDesc);
        //Debug.Log(itemTable.Get("Item2").GetSprite);
        //Debug.Log(itemTable.Get("Item2").GetDesc);
        //Debug.Log(itemTable.Get("Item3").GetSprite);
        //Debug.Log(itemTable.Get("Item3").GetDesc);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
