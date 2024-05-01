
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Select : GernericWindow
{
    public ToggleGroup toggleGroup;
    public UnityEngine.UI.Toggle[] toggles;
    public int activetoogleIndex;

    // Start is called before the first frame update
    void Start()
    {
        ActiveToogleCheck();

        activetoogleIndex = PlayerPrefs.GetInt("Level");
        toggles[activetoogleIndex].isOn = true;

    }

    public int ActiveToogleCheck()
    {
        toggles = GetComponentsInChildren<UnityEngine.UI.Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                activetoogleIndex = i;
                return i;
            }
        }

        return default;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSaveButton()
    {
        PlayerPrefs.SetInt("Level", activetoogleIndex);

        WindowManager.WindowManagerinstance.Open(0);
    }

    public void OnToggleValueChanged(bool active)
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) { activetoogleIndex = i;}
        }

        Debug.Log(activetoogleIndex);
    }


}
