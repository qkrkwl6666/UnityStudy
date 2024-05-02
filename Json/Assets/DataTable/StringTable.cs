using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Globalization;
using System.IO;

public class StringTable : DataTable
{
    private class Data
    {
        public string Id {  get; set;}
        public string String { get; set;}    
    }

    private Dictionary<string, string> table = new Dictionary<string, string>();

    public override void Load(string path)
    {
        path = string.Format(FormatPath, path);

        table.Clear();

        TextAsset testAsset = Resources.Load<TextAsset>(path);
        //Debug.Log(testAsset.text);

        // ��Ʈ�� ������ ��Ʈ���� �ѱ�
        using (var reader = new StringReader(testAsset.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<StringTable.Data>();

            foreach (var record in records)
            {
                table.Add(record.Id, record.String);
            }
        }
    }

    public string Get(string id)
    {
        if (!table.ContainsKey(id)) return string.Empty;

        return table[id];
    }
}
