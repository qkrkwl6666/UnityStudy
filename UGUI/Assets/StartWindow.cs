using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GernericWindow
{
    public Button buttonContiune;
    public Button buttonNewGame;
    public Button buttonOption;
    public bool canContiune;

    protected override void Awake()
    {
        buttonContiune.onClick.AddListener(OnclickContiune);
        buttonNewGame.onClick.AddListener(OnclickNewGame);
        buttonOption.onClick.AddListener(OnclickOption);

        base.Awake();
    }

    public override void Open()
    {
        buttonContiune.gameObject.SetActive(canContiune);

        firstSeleted = canContiune ? buttonContiune.gameObject : buttonNewGame.gameObject;

        base.Open();
    }

    public void OnclickContiune()
    {
        Debug.Log("OnclickContiune");

        WindowManager.WindowManagerinstance.Open(3);

    }

    public void OnclickNewGame()
    {
        Debug.Log("OnclickNewGame");

        WindowManager.WindowManagerinstance.Open(1);
    }

    public void OnclickOption()
    {
        Debug.Log("OnclickOption");

        WindowManager.WindowManagerinstance.Open(2);
    }
}
