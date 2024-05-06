using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = 50; // set the order in layer to 2
        int sortingLayerId = GetComponent<MeshRenderer>().sortingLayerID; // get the sorting layer ID.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
