using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;


public class UIItemSlot : MonoBehaviour
{
    private Button button;

    public Image itemIcon;
    public Image nullIcon;
    public TextMeshProUGUI itemName;

    public static event Action<int, ItemData> OnSelectItem;

    private ItemData data;
    public int slotIndex {  get; set; }

    private void Awake()
    {
        button = GetComponent<Button>();

    }

    public void SetEmpty()
    {
        itemName.text = "Empty";
        itemIcon = nullIcon;
        this.data = null;
        button.interactable = false;
    }

    public void SetData(ItemData data)
    {
        this.data = data;
        itemName.text = data.GetName;
        itemIcon.sprite = data.GetSprite;
        button.interactable = true;
    }

    public void OnClick()
    {
        if (data == null) return;

        OnSelectItem?.Invoke(slotIndex , this.data);
    }
}
