using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class UIInventory : MonoBehaviour
{
    public int currentSlotCount = 0;
    public int maxSlotCount = 30;

    public List<UIItemSlot> slots = new List<UIItemSlot>();

    public List<ItemData> itemDataList = new List<ItemData>(); // 원본 세이브 로드
    public List<ItemData> inventoryItemDataList = new List<ItemData>(); // 정렬 + 필터링

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

    private int sortingOption;
    private int filteringOption;

    public UIItemSlot inventorySlot;
    public GameObject content;

    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public Button addButton;
    public Button removeButton;

    ItemTable itemTable;

    private void UpdateItemSlots()
    {
        // 필터링 + 정렬
        inventoryItemDataList = itemDataList;

        switch (filteringOption)
        {
            case 0:
                break;
            case 1:
                inventoryItemDataList = inventoryItemDataList
                    .Where(data => data.Type == "Weapon")
                    .ToList();
                break;
            case 2:
                inventoryItemDataList = inventoryItemDataList
                    .Where(data => data.Type == "Equip")
                    .ToList();
                break;
            case 3:
                inventoryItemDataList = inventoryItemDataList
                    .Where(data => data.Type == "Consumable")
                    .ToList();
                break;
        }

        switch (sortingOption)
        {
            case 0:
                break;
            case 1:
                inventoryItemDataList = inventoryItemDataList
                    .OrderBy(data => data.GetName)
                    .ToList();
                break;
            case 2:
                inventoryItemDataList = inventoryItemDataList
                    .OrderByDescending(data => data.GetName)
                    .ToList();
                break;
            case 3:
                inventoryItemDataList = inventoryItemDataList
                    .OrderBy(data => data.Cost)
                    .ToList();
                break;
            case 4:
                inventoryItemDataList = inventoryItemDataList
                    .OrderByDescending(data => data.Cost)
                    .ToList();
                break;
        }



        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventoryItemDataList.Count)
            {
                slots[i].SetData(inventoryItemDataList[i]);
            }
            else
            {
                slots[i].SetEmpty();
            }

        }
    }

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
        sortingOption = 0;
        filtering.options.Clear();

        foreach (var optin in filteringOptions)
        {
            filtering.options.Add(new TMP_Dropdown.OptionData(optin));
        }

        filtering.value = 0;
        filteringOption = 0;
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
        sortingOption = value;

        UpdateItemSlots();
    }

    public void OnValueChangeFiltering(int value)
    {
        filteringOption = value;
        UpdateItemSlots();
    }



    public void OnClickItemAdd()
    {
        int RandomItem = UnityEngine.Random.Range(1, 5);

        if (currentSlotCount < maxSlotCount)
        {
            itemDataList.Add(itemTable.Get("Item" + RandomItem));
            UpdateItemSlots();
            currentSlotCount++;
            //slots[currentSlotCount++].SetData();
        }

    }

    public void OnClickItemRemove()
    {
        if (removeIndex == -1) return;

        itemDataList.RemoveAt(itemDataList.Count - 1);
        UpdateItemSlots();

        currentSlotCount--;
        removeIndex = -1;

        // for(int i = removeIndex + 1; i < slots.Count; i++)
        // {
        //     slots[i].slotIndex--;
        // }
        // 
        // var go = slots[removeIndex];
        // slots[removeIndex].gameObject.transform.SetAsLastSibling();
        // slots.RemoveAt(removeIndex);
        // slots.Add(go);
        // 
        // currentSlotCount--;
        // removeIndex = -1;
        // slots[maxSlotCount - 1].SetEmpty();
        // slots[maxSlotCount - 1].slotIndex = maxSlotCount - 1;
    }

    public void EventRemoveIndex(int index , ItemData data)
    {
        //Debug.Log(index);

        removeIndex = index;

        currentImage.sprite = slots[index].itemIcon.sprite;

        currentNameText.text = data.GetName;
        currentDescText.text = data.GetDesc;
        currentCostText.text = data.Cost.ToString();
        currentValueText.text = data.Value.ToString();
        currentTypeText.text = data.Type.ToString();
    }

    //public class 


}
