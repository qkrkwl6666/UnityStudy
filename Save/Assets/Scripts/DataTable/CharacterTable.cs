using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class CharacterTable : DataTable
{
    private Dictionary<string, CharacterData> table = new();
    public List<string> ids { get; private set; } = new();
    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
            return null;
        return table[id];
    }


    public override void Load(string path)
    {
        table.Clear();

        var textAsset = Resources.Load<TextAsset>(string.Format(FormatPath, path));

        using (var reader = new StringReader(textAsset.text))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csvReader.GetRecords<CharacterData>();
            foreach (var record in records)
            {
                table.Add(record.ID, record);
                ids.Add(record.ID);
            }
        }
    }
}