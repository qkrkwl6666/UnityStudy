using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadTest : MonoBehaviour
{
    public TMPro.TMP_InputField inputGold;
    public TMPro.TMP_InputField inputName;

    public GameObject cube;

    public void Save()
    {
        // Debug.Log(Application.persistentDataPath);
        // SaveDataV1 saveDataV1 = new SaveDataV1();
        // saveDataV1.Gold = int.Parse(inputGold.text);
        // SaveLoadSystem.Save(saveDataV1);

        // SaveDataV2 saveDataV2 = new SaveDataV2();
        // saveDataV2.Gold = int.Parse(inputGold.text);
        // saveDataV2.Name = inputName.text;
        // SaveLoadSystem.Save(saveDataV2);

        SaveDataV3 saveDataV3 = new SaveDataV3();

        saveDataV3.Rotation = cube.transform.rotation;
        saveDataV3.Scale = cube.transform.localScale;
        saveDataV3.Position = cube.transform.position;
        saveDataV3.color = cube.GetComponent<MeshRenderer>().material.color;

        saveDataV3.Gold = int.Parse(inputGold.text);
        SaveLoadSystem.Save(saveDataV3);
    }   
    public void Load()
    {
        SaveDataV3 saveData = (SaveDataV3)SaveLoadSystem.Load();
        
        inputGold.text = saveData.Gold.ToString();

        cube.transform.position = saveData.Position;
        cube.transform.rotation = saveData.Rotation;
        cube.transform.localScale = saveData.Scale;

        cube.GetComponent<MeshRenderer>().material.color = saveData.color;


        Debug.Log(saveData.Position);
        Debug.Log(saveData.Rotation);
        Debug.Log(saveData.color);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
