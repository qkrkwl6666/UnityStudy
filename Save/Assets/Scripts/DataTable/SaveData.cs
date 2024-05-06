using UnityEngine;

public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();
    public abstract SaveData VersionDown();
}

public class SaveData1 : SaveData
{
    public int Gold { get; set; } = 100;
    public SaveData1()
    {
        Version = 1;
    }
    public override SaveData VersionDown()
    {
        return null;
    }

    public override SaveData VersionUp()
    {
        return new SaveData2
        {
            Gold = Gold
        };
    }
}

public class SaveData2 : SaveData
{
    public int Gold { get; set; } = 100;
    public string Name { get; set; } = "string.Empty";

    public SaveData2()
    {
        Version = 2;
    }

    public override SaveData VersionDown()
    {
        return null;
    }

    public override SaveData VersionUp()
    {
        return new SaveData3
        {
            Gold = Gold
        };
    }
}

public class SaveData3 : SaveData
{
    public int Gold { get; set; } = 100;
    public Vector3 Position { get; set; } = Vector3.zero;
    public Quaternion Rotation { get; set; } = Quaternion.identity;
    public Vector3 Scale { get; set; } = Vector3.one;
    public Color color { get; set; } = Color.white;
    public SaveData3()
    {
        Version = 3;
    }

    public override SaveData VersionDown()
    {
        return null;
    }

    public override SaveData VersionUp()
    {
        return null;
    }
}
