using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    //public TextAsset testAsset;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            var stringTable = DataTableMgr.Get<StringTable>(DataTablesIds.String[i]);
            Debug.Log(stringTable.Get("HELLO"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
