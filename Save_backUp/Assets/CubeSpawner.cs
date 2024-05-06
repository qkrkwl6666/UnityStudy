using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeData
{
    public Vector3 position;
    public Quaternion rotation;
    public Color color;
    public Vector3 scale;
}

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public Mesh mesh;
    string temp;

    public void RandomSpawn()
    {
        GameObject newCube = Instantiate(cube);
        newCube.transform.position = Random.insideUnitSphere * 10;
        newCube.transform.rotation = Random.rotation;
        newCube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        newCube.transform.localScale = new Vector3(Random.value, Random.value, Random.value) * 2f;
    }

    public void Save()
    {
        List<CubeData> saveData = new();

        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var meshFilter = go.GetComponent<MeshFilter>();
            if (meshFilter == null
                || go.GetComponent<MeshRenderer>() == null)
                continue;

            if (meshFilter.sharedMesh != mesh)
                continue;
            
                CubeData cubeData = new CubeData();
                cubeData.position = go.transform.position;
                cubeData.rotation = go.transform.rotation;
                cubeData.color = go.GetComponent<MeshRenderer>().material.color;
                cubeData.scale = go.transform.localScale;
                saveData.Add(cubeData);
        }

        temp = JsonConvert.SerializeObject(saveData, Formatting.Indented,
                new Vector3Converter(), new QuaternionConverter(), new ColorConverter());

        File.WriteAllText(@"GameSave.json", temp);
    }


    public void Load()
    {
        if (!File.Exists(@"GameSave.json"))
            return;
        
        List<CubeData> loadData = new();
        loadData = JsonConvert.DeserializeObject<List<CubeData>>(File.ReadAllText(@"GameSave.json"),
            new Vector3Converter(), new QuaternionConverter(), new ColorConverter());

        Clear();

        foreach (var cubeData in loadData)
        {
            GameObject loadCube = Instantiate(cube);
            loadCube.transform.position = cubeData.position;
            loadCube.transform.rotation = cubeData.rotation;
            loadCube.transform.localScale = cubeData.scale;
            loadCube.GetComponent<MeshRenderer>().material.color = cubeData.color;
        }
    }

    public void Clear()
    {
        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var meshFilter = go.GetComponent<MeshFilter>();
            if (meshFilter == null
                || go.GetComponent<MeshRenderer>() == null)
                continue;

            if (meshFilter.sharedMesh == mesh)
            {
                Destroy(go);
            }
        }
    }
}
