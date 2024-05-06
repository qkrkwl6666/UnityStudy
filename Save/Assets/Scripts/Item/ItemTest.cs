using UnityEngine;

public class ItemTest : MonoBehaviour
{
    public ItemTable itemTable;
    // Start is called before the first frame update
    void Start()
    {
        var table = DataTableManager.Get<ItemTable>(DataTables.Item);
        Debug.Log(table.Get("Item3"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
