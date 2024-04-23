using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class QuaternionConverter : JsonConverter<UnityEngine.Quaternion>
{
    public override UnityEngine.Quaternion ReadJson(JsonReader reader, Type objectType, UnityEngine.Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        UnityEngine.Quaternion quaternion;

        quaternion.x = (float)jsonObject["z"];
        quaternion.y = (float)jsonObject["y"];
        quaternion.z = (float)jsonObject["z"];
        quaternion.w = (float)jsonObject["w"];


        return quaternion;
    }

    public override void WriteJson(JsonWriter writer, UnityEngine.Quaternion value, JsonSerializer serializer)
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
