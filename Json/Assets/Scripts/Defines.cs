using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Languages
{
    KOREAN,
    ENGLISH,
    JAPANESE,
}

public static class DataTablesIds
{
    public static readonly string[] String = 
        {"StringTableKR", "StringTableEN", "StringTableJP" };

    public static readonly string Item = "ItemTable";

    public static string CurrentString
    {
        get { return String[(int)Vars.currentLanguages]; }
    }
}

public static class Vars
{
    public static readonly string Version = "1.0.0";
    public static readonly int BuildVersion = 1;

    public static Languages currentLanguages = Languages.KOREAN;

    public static Languages editorLanguage = Languages.KOREAN;
}

public static class Tags
{
    public static readonly string Player = "Player";
    public static readonly string GameController = "GameController";
}

public static class Layers
{
    public static readonly string Default = "Default";
}

public static class SortingLayers
{
    public static readonly string UI = "UI";
}
