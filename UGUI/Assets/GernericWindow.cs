using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GernericWindow : MonoBehaviour
{
    public GameObject firstSeleted;
    protected EventSystem eventSystem;
    protected WindowManager windowManager;

    public void ServerInitialized(WindowManager windowManager)
    {
        this.windowManager = windowManager;
    }

    public EventSystem CurrentEventSystem
    {
        get 
        {
            if (eventSystem == null)
            {
                eventSystem = EventSystem.current;
            }
            return eventSystem; 
        }
    }
    protected virtual void Awake()
    {
        //Close();
    }

    public void OnFus()
    {
        CurrentEventSystem.SetSelectedGameObject(firstSeleted);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnFus();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }


    
}
