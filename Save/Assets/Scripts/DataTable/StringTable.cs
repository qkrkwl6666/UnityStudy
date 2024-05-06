using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }

    }

    Dictionary<string, string> table = new();

    public override void Load(string path)
    {
        table.Clear();

        var textAsset = Resources.Load<TextAsset>(string.Format(FormatPath, path));

        using (var reader = new StringReader(textAsset.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<Data>();
            foreach (var record in records)
            {
                table.Add(record.Id, record.String);
            }
        }
    }

    public string Get(string id)
    {
        if (!table.ContainsKey(id))
            return string.Empty;
        else
            return table[id];
    }
}
