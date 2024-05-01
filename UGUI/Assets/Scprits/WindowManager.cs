using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager WindowManagerinstance;

    public GernericWindow[] windows;

    public Windows defaultWindowId;
    private int currentWindowId;

    private void Start()
    {
        foreach (var window in windows)
        {
            window.gameObject.SetActive(false);
        }

        WindowManagerinstance = this;

        Open(defaultWindowId);
    }

    //public GernericWindow Get(int windowId)
    //{
    //    return windows[windowId];
    //}

    public GernericWindow Open(int windowId)
    {
        windows[currentWindowId].Close();
        currentWindowId = windowId;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }

    public GernericWindow Open(Windows windowId)
    {
        return Open((int)windowId);
    }
}
