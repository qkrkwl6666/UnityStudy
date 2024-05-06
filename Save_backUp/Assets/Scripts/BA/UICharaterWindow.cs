using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaterWindow : MonoBehaviour
{
    public UICharaterSlot prefab;
    public ScrollRect scrollRect;

    private CharacterTable table;
    private List<CharacterReal> characters = new();

    public void Start()
    {
        table = DataTableManager.Get<CharacterTable>(DataTables.Character);
    }

    public void Add()
    {
        var slot = Instantiate(prefab, scrollRect.content);
        var item = new CharacterReal();
        item.data = table.Get(table.ids[Random.Range(0, table.ids.Count)]);
        slot.SetCharacter(item);
        characters.Add(item);
    }
    public void Save()
    {

        SaveLoadSystem.CurrentData.characters = characters;
        SaveLoadSystem.Save();
    }

    public void Load()
    {
        var load = SaveLoadSystem.Load() as SaveData4;
        characters.Clear();
        characters = load.characters;

        for(int i = 0; i <  scrollRect.content.childCount; i++)
        {
            Destroy(scrollRect.content.GetChild(i).gameObject);
        }
        foreach (var c in characters)
        {
            var slot = Instantiate(prefab, scrollRect.content);
            slot.SetCharacter(c);
        }
    }
}
