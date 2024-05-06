using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese
}
public static class DataTables
{
    public static readonly string[] StringTable =
    {
        "StringTableKR",
        "StringTableEN",
        "StringTableJP"
    };

    public static readonly string Item = "ItemTable";
    public static readonly string Character = "CharacterTable";
}

public static class GameInfos
{
    public static readonly string Version = Application.version;
    public static readonly int BuildNumber = 1;
}


public static class Tags
{
    public static readonly string Player = "Player";
    public static readonly string GameController = "GameController";
}

public static class SortingLayers
{
    public static readonly string Default = "Default";
}

public static class Layers
{
    public static readonly string UI = "UI";
}

public static class Defines
{
    public static readonly string FormatIconPath = "Icons/Item/{0}";
}