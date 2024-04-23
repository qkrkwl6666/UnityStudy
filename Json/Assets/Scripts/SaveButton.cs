using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveButton : MonoBehaviour
{
    public RandomButton randomButton;
    public List<GameObject> cubeList;
    public static string path;

    public List<CubeData> cubeData = new List<CubeData>();

    // Start is called before the first frame update
    void Start()
    {
        randomButton = GameObject.FindWithTag("Button").GetComponent<RandomButton>();
        cubeList = randomButton.cubeList;
        path = Path.Combine(Application.persistentDataPath, "cubeData.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("SAVE");

        for (int i = 0; i < cubeList.Count; i++)
        {
            if (cubeList[i] == null)
            {
                cubeList.RemoveAt(i);
                break;
            }

            CubeData data = new CubeData
            {
                position = cubeList[i].transform.position,
                scale = cubeList[i].transform.localScale,
                rotate = cubeList[i].transform.rotation,
                color = cubeList[i].GetComponent<MeshRenderer>().material.color,
            };

            cubeData.Add(data);
            
        }

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new QuaternionConverter(),
                new Vector3Converter(),
                new ColorConverter(),
            }
        };
        

        string json = JsonConvert.SerializeObject(cubeData, settings);

        File.WriteAllText(path, json);

        Debug.Log(path);
    }
}
