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
    public Sprite nullIcon;
    public TextMeshProUGUI itemName;

    public static event Action<int, ItemData> OnSelectItem;

    public SaveItemData saveItemData { get; set; }
    private ItemData data;
    public int slotIndex {  get; set; }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetEmpty()
    {
        itemName.text = "Empty";
        itemIcon.sprite = nullIcon;
        saveItemData = null;

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

    public void SetData(SaveItemData Itemdata)
    {
        saveItemData = Itemdata;

        this.data = saveItemData.data;
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
