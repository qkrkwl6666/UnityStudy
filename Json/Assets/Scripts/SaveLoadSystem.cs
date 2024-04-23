using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using Newtonsoft.Json;

public static class SaveLoadSystem
{
    public enum Mode
    {
        Json,
        Binary,
        EncryptedBinary,
    }

    public static Mode FileMode { get; set; } = Mode.Json;

    public static int SaveDataVersion { get; private set; } = 3;

    // 0 (자동) , 1 , 2,  3...

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.sav",
        "Save1.sav",
        "Save2.sav",
        "Save3.sav",
    };

    private static string SaveDirectory
    {
        get { return $"{Application.persistentDataPath}/Save"; }
    }

    public static bool Save(SaveData saveData, int slot = 0)
    {
        if(slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }

        if(!System.IO.Directory.Exists(SaveDirectory))
        {
            System.IO.Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);

        // FileMode 분기 일단 Json 처리

        using(var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serializer = new JsonSerializer();

            serializer.Converters.Add(new Vector3Converter());
            serializer.Converters.Add(new QuaternionConverter());
            serializer.Converters.Add(new ColorConverter());

            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Serialize(writer, saveData);
        }

        return true;
    }

    public static SaveData Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileName.Length)
        {
            return null;
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);

        if (!System.IO.File.Exists(Path.Combine(SaveDirectory, SaveFileName[slot])))
        {
            return null;
        }

        using (var writer = new JsonTextReader(new StreamReader(path)))
        {
            var serializer = new JsonSerializer();
            //serializer.Formatting = Formatting.Indented;

            serializer.Converters.Add(new Vector3Converter());
            serializer.Converters.Add(new QuaternionConverter());
            serializer.Converters.Add(new ColorConverter());

            serializer.TypeNameHandling = TypeNameHandling.All;

            SaveData saveData = serializer.Deserialize<SaveData>(writer);

            while(saveData.Version < SaveDataVersion)
            {
                saveData = saveData.VersionUp();
            }

            return saveData;
        }

    }
}
