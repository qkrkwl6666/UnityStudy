using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{


    private readonly List<string> sortingOptions =
    new() {
        "이름 오름차순",
        "이름 내림차순",
        "가격 오름차순",
        "가격 내림차순",
        "커스텀"
    };

    private readonly List<string> filteringOptions =
    new() {
        "전부",
        "Weapon",
        "Eqiup",
        "Consumable"
    };

    private int sortingOption;
    private int filteringOption;

    public UIItemSlot itemSlot;
    public ScrollRect scrollRect;

    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public Button add;
    public Button remove;

    public UIItemInfo itemInfo;

    private ItemTable table;

    public int maxSlot = 30;
    private List<UIItemSlot> slots = new();
    private int usingSlotCount;
    private int selectedSlotIndex = -1;

    private List<SaveItemData> itemDataList = new();
    private List<SaveItemData> inventoryItemList = new();
    private void UpdateItemSlots()
    {
        inventoryItemList.Clear();
        //필터링
        foreach (var item in itemDataList)
        {
            if (filteringOption == 0 || item.data.Type == filteringOptions[filteringOption])
                inventoryItemList.Add(item);
        }
        //정렬
        switch (sortingOption)
        {
            case 0:
                inventoryItemList.Sort((x, y) => x.data.GetName.CompareTo(y.data.GetName));
                break;
            case 1:
                inventoryItemList.Sort((x, y) => y.data.GetName.CompareTo(x.data.GetName));
                break;
            case 2:
                inventoryItemList.Sort((x, y) => x.data.Price.CompareTo(y.data.Price));
                break;
            case 3:
                inventoryItemList.Sort((x, y) => y.data.Price.CompareTo(x.data.Price));
                break;
            case 4:
                inventoryItemList.Sort((x, y) => x.customOrder.CompareTo(y.customOrder));
                break;
        }


        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventoryItemList.Count)
                slots[i].SetData(inventoryItemList[i]);
            else
                slots[i].SetEmpty();
        }
    }

    private void Awake()
    {
        for (int i = 0; i < maxSlot; i++)
        {
            var slot = Instantiate(itemSlot, scrollRect.content);
            slot.SetEmpty();
            slot.SlotIndex = i;
            slot.button.onClick.AddListener(() =>
            {
                itemInfo.SetData(slot.saveData);
                selectedSlotIndex = slot.SlotIndex;
            });

            slots.Add(slot);
        }

        sorting.ClearOptions();
        sorting.AddOptions(sortingOptions);
        sortingOption = sorting.value = 0;
        sorting.RefreshShownValue();

        filtering.ClearOptions();
        filtering.AddOptions(filteringOptions);
        filteringOption = filtering.value = 0;
        filtering.RefreshShownValue();

        sorting.onValueChanged.AddListener(OnValueChangeSorting);
        filtering.onValueChanged.AddListener(OnValueChangeFiltering);
        add.onClick.AddListener(OnClickItemAdd);
        remove.onClick.AddListener(OnClickItemRemove);

        itemInfo.SetEmpty();
    }

    private void Start()
    {
        table = DataTableManager.Get<ItemTable>(DataTables.Item);
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
        if (usingSlotCount < maxSlot)
        {
            var ranID = Random.Range(0, table.ids.Count);
            var item = new SaveItemData();
            item.data = table.Get(table.ids[ranID]);
            item.customOrder = usingSlotCount++;

            itemDataList.Add(item);
            
            UpdateItemSlots();

            //slots[usingSlotCount++]
            //    .SetData(table
            //    .Get(table.ids[Random.Range(0, table.ids.Count)]));
        }

    }
    public void OnClickItemRemove()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
        {
            itemDataList.RemoveAll(x => x.instanceId == inventoryItemList[selectedSlotIndex].instanceId);
            UpdateItemSlots();
            usingSlotCount--;
            itemInfo.SetEmpty();
            selectedSlotIndex = -1;
        }
        //if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
        //{
        //    slots[selectedSlotIndex].SetEmpty();
        //    slots[selectedSlotIndex].transform.SetSiblingIndex(usingSlotCount - 1);

        //    for (int i = selectedSlotIndex; i < usingSlotCount - 1; i++)
        //    {
        //        (slots[i], slots[i + 1]) = (slots[i + 1], slots[i]);
        //        (slots[i].SlotIndex, slots[i + 1].SlotIndex) = (slots[i + 1].SlotIndex, slots[i].SlotIndex);

        //    }
        //    --usingSlotCount;
        //    selectedSlotIndex = -1;

        //    itemInfo.SetEmpty();
        //}
    }



    public void SaveInventory()
    {

        SaveLoadSystem.CurrentData.filter = filteringOption;
        SaveLoadSystem.CurrentData.sort = sortingOption;
        SaveLoadSystem.CurrentData.items = itemDataList;
        SaveLoadSystem.Save();
    }

    public void LoadInventory()
    {
        var load = SaveLoadSystem.Load() as SaveData4;
        sortingOption = sorting.value = load.sort;
        filteringOption = filtering.value = load.filter;
        itemDataList.Clear();
        itemDataList = load.items;
        UpdateItemSlots();
    }
}
