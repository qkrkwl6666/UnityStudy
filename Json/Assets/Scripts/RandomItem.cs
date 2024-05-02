using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public UIItemSlot inventorySlot;
    public GameObject content;

    public int currentSlotCount = 0;
    public int maxSlotCount = 30;
    public List<UIItemSlot> slots = new List<UIItemSlot>();

    public const int ab = 3;
    public readonly int abc = 3;

    ItemTable itemTable;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            inventorySlot.SetEmpty();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            

            //var go = Instantiate(inventorySlot , content.transform);
            //go.SetData(itemTable.Get("Item" + RandomItem));
            //inventorySlot.SetData(itemTable.Get("Item1"));
        }
    }
}
