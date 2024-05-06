using System.Collections.Generic;

public static class DataTableManager
{
    private static Dictionary<string, DataTable> tables = new();

    static DataTableManager()
    {
        for (int i = 0; i < DataTables.StringTable.Length; i++)
        {
            var table = new StringTable();
            table.Load(DataTables.StringTable[i]);
            tables.Add(DataTables.StringTable[i], table);
        }

        var iTable = new ItemTable();
        iTable.Load(DataTables.Item);
        tables.Add(DataTables.Item, iTable);


        var cTable = new CharacterTable();
        cTable.Load(DataTables.Character);
        tables.Add(DataTables.Character, cTable);
    }

    public static StringTable GetStringTable()
    {
        return Get<StringTable>(DataTables.StringTable[(int)Languages.Korean]);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
            return null;
        else
            return tables[id] as T;
    }
}
