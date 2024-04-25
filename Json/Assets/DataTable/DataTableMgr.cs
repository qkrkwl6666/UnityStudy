using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTableMgr
{
    private static Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableMgr()
    {
        DataTable table = new StringTable();
        table.Load(DataTablesIds.String[(int)Languages.KOREAN]);
        tables.Add(DataTablesIds.String[(int)Languages.KOREAN], table);

        DataTable table2 = new StringTable();
        table2.Load(DataTablesIds.String[(int)Languages.ENGLISH]);
        tables.Add(DataTablesIds.String[(int)Languages.ENGLISH], table2);

        DataTable table3 = new StringTable();
        table3.Load(DataTablesIds.String[(int)Languages.JAPANESE]);
        tables.Add(DataTablesIds.String[(int)Languages.JAPANESE], table3);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        // 매개변수로온 타입 가져오기
        //var id = typeof(T);

        if (!tables.ContainsKey(id)) return null;

        return tables[id] as T;
    }
}
