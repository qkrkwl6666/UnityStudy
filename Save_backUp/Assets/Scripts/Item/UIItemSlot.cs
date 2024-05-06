using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public int SlotIndex { get; set; }

    public Button button;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;

    public ItemData data { get; private set; }
    public SaveItemData saveData { get; private set; }

    public void SetEmpty()
    {
        data = null;
        saveData = null;
        itemIcon.sprite = null;
        itemName.text = string.Empty;
        itemDesc.text = string.Empty;
        button.interactable = false;
    }

    public void SetData(SaveItemData savedata)
    {
        this.saveData = savedata;
        this.data = saveData.data;

        itemIcon.sprite = data.GetIconSprite;
        itemName.text = data.GetName;
        itemDesc.text = data.GetDesc;
        button.interactable = true;
    }

    public void OnClick()
    {
        Debug.Log($"{data?.Id}, {SlotIndex}, {transform.GetSiblingIndex()}");
    }
}
