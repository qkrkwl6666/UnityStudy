using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Json2 : MonoBehaviour
{
    public string output;
    MyClass myObject = new MyClass();
    [Serializable]
    public class MyClass
    {
        public int level;
        public float timeElapsed;
        public string playerName;
    }

    void Start()
    {
        List<MyClass> list = new List<MyClass>();

        MyClass myObject = new MyClass();
        myObject.level = 1;
        myObject.timeElapsed = 47.5f;
        myObject.playerName = "Dr Charles Francis";

        list.Add(myObject);
        list.Add(myObject);

        Debug.Log(list[0] == list[1]);

        output = JsonConvert.SerializeObject(list);
        Debug.Log(output);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    MyClass myObject = JsonConvert.DeserializeObject<MyClass>(output);
        //    Debug.Log(myObject.level);
        //    Debug.Log(myObject.timeElapsed);
        //    Debug.Log(myObject.playerName);
        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            List<MyClass> list = JsonConvert.DeserializeObject<List<MyClass>>(output);
            Debug.Log(list[0].level);
            Debug.Log(list[0].timeElapsed);
            Debug.Log(list[0].playerName);

            // Json 파일의 배열을 서로 다른 인스턴스가 생성된다.
            // 참조형들을 저장할 때 다시 불러올때 참조를 그대로 불러와야하는건지
            // or 인스턴스만 생성되면 되는건지 주의 해야함

            Debug.Log(list[0] == list[1]);
        }
    }
}
