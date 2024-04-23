using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    public GameObject cube;

    public List<GameObject> cubeList;

    public List<CubeData> cubeData;

    void Start()
    {
        var randomButton = GameObject.FindWithTag("Button").GetComponent<RandomButton>();
        cubeList = randomButton.cubeList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        string path = SaveButton.path;
        Debug.Log("Load");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>
                {
                    new Vector3Converter(),
                    new ColorConverter(),
                    new QuaternionConverter(),
                }
            };

            for(int i = 0; i < cubeList.Count; i++)
            {
                Destroy(cubeList[i]);
            }

            cubeList.Clear();

            cubeData = JsonConvert.DeserializeObject<List<CubeData>>(json, settings);

            for(int i = 0; i < cubeData.Count; i++)
            {
                Vector3 randomPos = cubeData[i].position;
                Quaternion randomQuat = cubeData[i].rotate;
                var go = GameObject.Instantiate(cube, randomPos, randomQuat);
                go.transform.localScale = cubeData[i].scale;
                go.GetComponent<MeshRenderer>().material.color = cubeData[i].color;
                cubeList.Add(go);
            }

        }
    }
}
