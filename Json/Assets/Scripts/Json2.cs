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

            // Json ������ �迭�� ���� �ٸ� �ν��Ͻ��� �����ȴ�.
            // ���������� ������ �� �ٽ� �ҷ��ö� ������ �״�� �ҷ��;��ϴ°���
            // or �ν��Ͻ��� �����Ǹ� �Ǵ°��� ���� �ؾ���

            Debug.Log(list[0] == list[1]);
        }
    }
}
