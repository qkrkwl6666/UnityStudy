using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class ColorConverter : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        Color c = new Color();
        c.r = (float)jo["r"];
        c.g = (float)jo["g"];
        c.b = (float)jo["b"];
        c.a = (float)jo["a"];
        return c;
    }
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("r");
        writer.WriteValue(value.r);
        writer.WritePropertyName("g");
        writer.WriteValue(value.g);
        writer.WritePropertyName("b");
        writer.WriteValue(value.b);
        writer.WritePropertyName("a");
        writer.WriteValue(value.a);
        writer.WriteEndObject();
    }
}


public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        Quaternion q = new Quaternion(
            (float)jo["x"],
            (float)jo["y"],
            (float)jo["z"],
            (float)jo["w"]);
        return q;
    }
    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.z);
        writer.WritePropertyName("w");
        writer.WriteValue(value.w);
        writer.WriteEndObject();
    }
}


public class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        Vector2 v = new Vector2();
        v.x = (float)jo["x"];
        v.y = (float)jo["y"];
        return v;
    }
    public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WriteEndObject();
    }
}


public class Vector3Converter : JsonConverter<Vector3>
{
    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        Vector3 v = new Vector3();
        v.x = (float)jo["x"];
        v.y = (float)jo["y"];
        v.z = (float)jo["z"];
        return v;
    }
    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.z);
        writer.WriteEndObject();
    }
}

public class SaveItemDataConverter : JsonConverter<SaveItemData>
{
    public override SaveItemData ReadJson(JsonReader reader, Type objectType, SaveItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        SaveItemData sid = new SaveItemData();
        sid.instanceId = (int)jo["instanceId"];
        sid.creationTime = (DateTime)jo["creationTime"];
        sid.customOrder = (int)jo["customOrder"];
        sid.data = DataTableManager.Get<ItemTable>(DataTables.Item).Get((string)jo["dataId"]);
        return sid;
    }

    public override void WriteJson(JsonWriter writer, SaveItemData value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("instanceId");
        writer.WriteValue(value.instanceId);
        writer.WritePropertyName("creationTime");
        writer.WriteValue(value.creationTime);
        writer.WritePropertyName("customOrder");
        writer.WriteValue(value.customOrder);
        writer.WritePropertyName("dataId");
        writer.WriteValue(value.data.Id);
        writer.WriteEndObject();
    }
}

public class CharacterRealConverter : JsonConverter<CharacterReal>
{
    public override CharacterReal ReadJson(JsonReader reader, Type objectType, CharacterReal existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        CharacterReal cr = new CharacterReal();
        cr.ID = (int)jo["ID"];
        cr.data = DataTableManager.Get<CharacterTable>(DataTables.Character).Get((string)jo["dataID"]);
        return cr;
    }

    public override void WriteJson(JsonWriter writer, CharacterReal value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("ID");
        writer.WriteValue(value.ID);
        writer.WritePropertyName("dataID");
        writer.WriteValue(value.data.ID);
        writer.WriteEndObject();
    }
}