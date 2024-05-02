using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIInventory : MonoBehaviour
{
    public int currentSlotCount = 0;
    public int maxSlotCount = 30;
    public List<UIItemSlot> slots = new List<UIItemSlot>();

    public static int removeIndex = -1;
    public Image currentImage;
    public TMPro.TextMeshProUGUI currentNameText;
    public TMPro.TextMeshProUGUI currentDescText;
    public TMPro.TextMeshProUGUI currentCostText;
    public TMPro.TextMeshProUGUI currentValueText;
    public TMPro.TextMeshProUGUI currentTypeText;

    private readonly string[] sortingOptions =
    {
        "커스텀",
        "이름 오름차순",
        "이름 내림차순",
        "가격 오름차순",
        "가격 내림차순",
    };

    private readonly string[] filteringOptions =
    {
        "전부",
        "무기",
        "장비",
        "소모품",
    };

    public UIItemSlot inventorySlot;
    public GameObject content;

    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public Button addButton;
    public Button removeButton;

    ItemTable itemTable;

    private void Awake()
    {
        itemTable = DataTableMgr.Get<ItemTable>(DataTablesIds.Item);

        sorting.options.Clear();

        foreach(var optin in sortingOptions)
        {
            sorting.options.Add(new TMP_Dropdown.OptionData(optin));
        }

        sorting.value = 0;
        sorting.RefreshShownValue();
        filtering.options.Clear();

        foreach (var optin in filteringOptions)
        {
            filtering.options.Add(new TMP_Dropdown.OptionData(optin));
        }

        filtering.value = 0;
        filtering.RefreshShownValue();
    }

    private void Start()
    {
        itemTable = DataTableMgr.Get<ItemTable>(DataTablesIds.Item);

        for (int i = 0; i < maxSlotCount; i++)
        {
            slots.Add(Instantiate(inventorySlot, content.transform));
            slots[i].SetEmpty();
            slots[i].slotIndex = i;
        }

        UIItemSlot.OnSelectItem += EventRemoveIndex;
    }

    private void Update()
    {
        
    }

    public void OnValueChangeSorting(int value)
    {
        Debug.Log(sortingOptions[value]);
    }

    public void OnValueChangeFiltering(int value)
    {
        Debug.Log(filteringOptions[value]);
    }

    public void OnClickItemAdd()
    {
        int RandomItem = UnityEngine.Random.Range(1, 5);

        if (currentSlotCount < maxSlotCount)
        {
            slots[currentSlotCount++].SetData(itemTable.Get("Item" + RandomItem));
        }

    }

    public void OnClickItemRemove()
    {
        if (removeIndex == -1) return;

        for(int i = removeIndex + 1; i < slots.Count; i++)
        {
            slots[i].slotIndex--;
        }

        Destroy(slots[removeIndex].gameObject);
        slots.RemoveAt(removeIndex);

        currentSlotCount--;
        removeIndex = -1;

        slots.Add(Instantiate(inventorySlot, content.transform));
        slots[slots.Count - 1].SetEmpty();
    }

    public void EventRemoveIndex(int index , ItemData data)
    {
        Debug.Log(index);

        removeIndex = index;

        currentImage.sprite = slots[index].itemIcon.sprite;

        currentNameText.text = data.GetName;
        currentDescText.text = data.GetDesc;
        currentCostText.text = data.Cost.ToString();
        currentValueText.text = data.Value.ToString();
        currentTypeText.text = data.Type.ToString();
    }
}
