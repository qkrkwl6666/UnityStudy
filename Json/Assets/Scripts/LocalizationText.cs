using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    public Languages language;
    public string textId;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            OnChangeLanguage(Vars.currentLanguages);
        }
        else
        {
            OnChangeLanguage(Vars.editorLanguage);
        }

    }

    public void OnChangeLanguage(Languages lang)
    {
        var stringTable = DataTableMgr.Get<StringTable>(DataTablesIds.String[(int)lang]);
        text.text = stringTable.Get(textId);
    }
}
