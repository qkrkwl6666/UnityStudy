using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    static SaveLoadSystem()
    {
        CurrentData = new SaveData4();
    }

    public enum Mode
    {
        Json,
        Binary,
        EncrpytedBinary
    }

    public static Mode FileMode { get; set; } = Mode.Json;
    public static int SaveDataVersion { get; private set; } = 4;

    public static readonly string[] SaveFileName =
    {
        "SaveAuto.sav",
        "Save1.sav",
        "Save2.sav",
        "Save3.sav"
    };
    public static SaveData4 CurrentData { get; set; }
    public static int currentSlot;

    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";
        }
    }

    public static bool Save(int slot = -1, SaveData data = null)
    {
        if (data == null)
            data = CurrentData;

        if (slot == -1)
            slot = currentSlot;

        if (slot < 0 || slot >= SaveFileName.Length)
            return false;

        if (!Directory.Exists(SaveDirectory))
            Directory.CreateDirectory(SaveDirectory);

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serializer = new JsonSerializer();

            serializer.Converters.Add(new ColorConverter());
            serializer.Converters.Add(new Vector3Converter()); 
            serializer.Converters.Add(new Vector2Converter());
            serializer.Converters.Add(new QuaternionConverter());
            serializer.Converters.Add(new SaveItemDataConverter());

            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Serialize(writer, data);
        }


        //switch (FileMode)
        //{
        //    case Mode.Json:
        //        break;
        //    case Mode.Binary:
        //        break;
        //    case Mode.EncrpytedBinary:
        //        break;
        //}
        CurrentData = data as SaveData4;
        currentSlot = slot;
        return true;
    }

    public static SaveData Load(int slot = -1, SaveData data = null)
    {
        if (data == null)
            data = CurrentData;

        if (slot == -1)
            slot = currentSlot;

        if (slot < 0 || slot >= SaveFileName.Length)
            return null;

        if (!Directory.Exists(SaveDirectory))
            return null;

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        using (var writer = new JsonTextReader(new StreamReader(path)))
        {
            var serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Converters.Add(new ColorConverter());
            serializer.Converters.Add(new Vector3Converter());
            serializer.Converters.Add(new Vector2Converter());
            serializer.Converters.Add(new QuaternionConverter());
            serializer.Converters.Add(new SaveItemDataConverter());
            var saveData = serializer.Deserialize<SaveData>(writer);
            while (saveData.Version != SaveDataVersion)
            {
                if (saveData.Version < SaveDataVersion)
                {
                    saveData = saveData.VersionUp();
                }
                else if (saveData.Version > SaveDataVersion)
                {
                    saveData = saveData.VersionDown();
                }
            }
            CurrentData = data as SaveData4;
            currentSlot = slot;
            return saveData;
        }
    }
}
