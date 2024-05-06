using TMPro;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    public TMP_InputField inputGold;
    public TMP_InputField inputName;
    public GameObject cube;

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        SaveData3 data = new SaveData3();
        data.Gold = int.Parse(inputGold.text);
        data.Position = cube.transform.position;
        data.Rotation = cube.transform.rotation;
        data.Scale = cube.transform.localScale;
        data.color = cube.GetComponent<MeshRenderer>().material.color;
        SaveLoadSystem.Save(0, data);
    }

    public void Load()
    {
        var load = SaveLoadSystem.Load() as SaveData3;
        inputGold.text = load.Gold.ToString();
        cube.transform.position = load.Position;
        cube.transform.rotation = load.Rotation;
        cube.transform.localScale = load.Scale;
        cube.GetComponent<MeshRenderer>().material.color = load.color;
    }
}
