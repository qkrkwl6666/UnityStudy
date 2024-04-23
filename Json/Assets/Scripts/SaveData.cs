using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public int Gold { get; set; } = 100;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        SaveDataV2 saveData = new SaveDataV2();
        saveData.Gold = Gold;

        return saveData;
    }
}

public class SaveDataV2 : SaveData
{
    public int Gold { get; set; } = 100;
    public string Name { get; set; } = "Park";
    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        SaveDataV3 saveData = new SaveDataV3();
        saveData.Gold = Gold;
        return saveData;
    }
}

public class SaveDataV3 : SaveData
{
    public int Gold { get; set; } = 100;
    //public string Name { get; set; } = "Park";
    public Vector3 Position { get; set; } = Vector3.zero;
    public Quaternion Rotation { get; set; } = Quaternion.identity;
    public Vector3 Scale { get; set; } = Vector3.one;
    public Color color { get; set; } = Color.blue;

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        return null;
    }
}

