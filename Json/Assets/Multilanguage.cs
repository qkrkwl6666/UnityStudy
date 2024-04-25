using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multilanguage : MonoBehaviour
{
    public Languages currentLanguages;
    public string id;
    public List<string> ids = new List<string>();
    private TMPro.TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        currentLanguages = Languages.KOREAN;
        id = "HELLO";
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
        ids.Add("HELLO");
        ids.Add("BYE");
        ids.Add("YOU DIE");
        ids.Add("TITLE");
    }

    // Start is called before the first frame update
    void Start()
    {
        changeLanguages();
        StartCoroutine(CoChangeLanguagesUI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeLanguages()
    {
        var stringTable = DataTableMgr.Get<StringTable>(DataTablesIds.String[(int)currentLanguages]);

        Debug.Log(stringTable.Get(id));
        textMeshPro.text = stringTable.Get(id);

        Debug.Log("changeLanguages");
    }

    IEnumerator CoChangeLanguagesUI()
    {
        while (true)
        {

            yield return new WaitForSeconds(1f);

            Debug.Log("CoChangeLanguagesUI");

            currentLanguages = (Languages)Random.Range(0, 3);
            id = ids[Random.Range(0, 4)];

            changeLanguages();
        }
    }
}
