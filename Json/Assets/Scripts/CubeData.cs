using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class CubeData : ScriptableObject
{

    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotate;
    public Color color;

}
