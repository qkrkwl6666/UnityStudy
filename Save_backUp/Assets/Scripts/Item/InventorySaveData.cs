using System.Collections.Generic;
public class SaveData4 : SaveData
{
    public List<SaveItemData> items;
    public int sort;
    public int filter;

    public List<CharacterReal> characters;

    public SaveData4()
    {
        Version = 4;
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