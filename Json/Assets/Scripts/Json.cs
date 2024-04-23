using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyClass
{
    public int level;
    public float timeElapsed;
    public string playerName;
}

public class Json : MonoBehaviour
{
    private string json;

    // Start is called before the first frame update
    void Start()
    {
        var array = new List<MyClass>();

        MyClass myObject = new MyClass();
        myObject.level = 1;
        myObject.timeElapsed = 47.5f;
        myObject.playerName = "Dr Charles Francis";

        array.Add(myObject);

        myObject = new MyClass();
        myObject.level = 2;
        myObject.timeElapsed = 57.5f;
        myObject.playerName = "dawddw";

        array.Add(myObject);

        json = JsonUtility.ToJson(myObject);
        Debug.Log(json);
        // json now contains: '{"level":1,"timeElapsed":47.5,"playerName":"Dr Charles Francis"}'
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //MyClass myObject = JsonUtility.FromJson<MyClass>(json);
            //Debug.Log(myObject.level);
            //Debug.Log(myObject.timeElapsed);
            //Debug.Log(myObject.playerName);

            // MyClass myObject = new MyClass();
            // JsonUtility.FromJsonOverwrite(json, myObject);
            
        }
    }
}
