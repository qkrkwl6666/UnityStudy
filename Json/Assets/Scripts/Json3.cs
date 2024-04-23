using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Unity.VisualScripting;

public class Json3 : MonoBehaviour
{
    private string json;

    // Update is called once per frame
    private void Start()
    {
        Vector3 position = transform.position;
        json = JsonConvert.SerializeObject(position , Formatting.None , new Vector3Converter());
        Debug.Log(json);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 pos = JsonConvert.DeserializeObject<Vector3>(json, new Vector3Converter());
            Debug.Log(pos);
        }


    }
}
