using CsvHelper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class ItemData
{
    
    public string Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Icon { get; set; }
    public int Value { get; set; }
    public int Price { get; set; }

    [JsonIgnore]
    public string GetName => DataTableManager.GetStringTable().Get(Name);
    [JsonIgnore]
    public string GetDesc => DataTableManager.GetStringTable().Get(Desc);
    [JsonIgnore]
    public Sprite GetIconSprite => Resources.Load<Sprite>(string.Format(Defines.FormatIconPath, Icon));

    public override string ToString()
    {
        return $"{Id}, {Type}, {GetName}, {GetDesc}, {GetIconSprite}, {Value}, {Price}";
    }

}


public class ItemTable : DataTable
{
    private Dictionary<string, ItemData> table = new();
    public List<string> ids { get; private set; } = new();
    public ItemData Get(string id)
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
            var records = csvReader.GetRecords<ItemData>();
            foreach (var record in records)
            {
                table.Add(record.Id, record);
                ids.Add(record.Id);
            }
        }
    }
}
