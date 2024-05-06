using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NikkeInfo : MonoBehaviour
{
    private VideoPlayer player;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
