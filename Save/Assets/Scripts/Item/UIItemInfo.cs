using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI type;
    public TextMeshProUGUI value;
    public TextMeshProUGUI price;
    public TextMeshProUGUI desc;

    public void SetData(SaveItemData item)
    {
        image.sprite = item.data.GetIconSprite;
        itemName.text = item.data.GetName;
        type.text = item.data.Type;
        value.text = $"{item.data.Value:0}";
        price.text = $"{item.data.Price:0}";
        desc.text = item.data.GetDesc;
    }

    public void SetEmpty()
    {
        image.sprite = null;
        itemName.text = string.Empty;
        type.text = string.Empty;
        value.text = string.Empty;
        price.text = string.Empty;
        desc.text = string.Empty;
    }
}
