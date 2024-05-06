using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[ExecuteInEditMode]
public class TextLocalization : MonoBehaviour
{
    TextMeshProUGUI text;
    public Languages language;
    public string id = "HELLO";

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


    public void SetText()
    {
        var stringTable = DataTableManager.Get<StringTable>(DataTables.StringTable[(int)language]);
        text.text = stringTable.Get(id);
    }
}
