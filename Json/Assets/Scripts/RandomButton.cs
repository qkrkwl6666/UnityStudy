using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomButton : MonoBehaviour
{
    public GameObject cube;

    public List<GameObject> cubeList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Vector3 randomPos = new Vector3();

        randomPos.x = Random.Range(0, 1350);
        randomPos.y = Random.Range(0, 520);
        GameObject newCube = GameObject.Instantiate(cube , randomPos, Random.rotation);
        newCube.transform.localScale = new Vector3(Random.Range(50,100), Random.Range(50, 100), Random.Range(50, 100));
        newCube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

        cubeList.Add(newCube);
    }
}
