using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Text;
using Unity.VisualScripting;

public class KeyboardWindow : GernericWindow
{
    public bool isActive = true;
    public TextMeshProUGUI textMeshProUGUI;
    public int stringCount = 10;
    private StringBuilder sb = new StringBuilder();
    private float textActiveTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Active());
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive && textMeshProUGUI.text.Length >= 10)
        {
            return;
        }


        if(textActiveTime <= Time.time)
        {
            sb.Clear();
            textActiveTime = Time.time + 1f;
            sb.Insert(0, textMeshProUGUI.text);

            if (isActive)
            {
                textMeshProUGUI.text = sb.Remove(sb.Length - 1, 1).ToString();
                isActive = false;
            }
            else
            {
                textMeshProUGUI.text = sb.Insert(sb.Length, "_").ToString();
                isActive = true;
            }

            sb.Clear();
        }
    }

    public void KeyboardButton()
    {
        if(!isActive)
        {
            if (textMeshProUGUI.text.Length >= 10) return;

            textMeshProUGUI.text += CurrentEventSystem.currentSelectedGameObject.name;
        }
        else
        {
            if (textMeshProUGUI.text.Length >= 11) return;

            sb.Clear();

            sb.Insert(0, textMeshProUGUI.text);

            textMeshProUGUI.text = sb.Insert(sb.Length - 1, 
                CurrentEventSystem.currentSelectedGameObject.name).ToString();
        }
        
    }

    public void CancelButton()
    {
        if (!isActive)
            textMeshProUGUI.text = "";
        else if (isActive)
            textMeshProUGUI.text = "_";
    }

    public void DeleteButton()
    {
        sb.Clear();

        sb.Insert(0, textMeshProUGUI.text);

        int i = isActive ? 1 : 0;

        if (sb.Length <= i) return;

        if (isActive)
        {
            sb.Remove(sb.Length - 2, 1);
            textMeshProUGUI.text = sb.ToString();
        }
        else
        {
            sb.Remove(sb.Length - 1, 1);
            textMeshProUGUI.text = sb.ToString();
        }



    }

    public void Accept()
    {
        WindowManager.WindowManagerinstance.Open(0);
    }

    IEnumerator Active()
    {
        while (isActive)
        {
            Debug.Log("Active");

            yield return new WaitForSeconds(1f);
        }
    }


}
