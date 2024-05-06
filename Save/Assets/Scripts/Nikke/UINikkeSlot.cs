using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Status
{
    IDLE,
    COVER,
    AIM,
}


public class UINikkeSlot : MonoBehaviour
{
    public Sprite nikkeImg;
    public NikkeData nikkeData;

    public List<SkeletonDataAsset> skeletonDataAsset = new List<SkeletonDataAsset>();

    public NikkeInfo nikkeInfo;

    private void Awake()
    {
        nikkeData = new NikkeData();
        nikkeData.NikkeName = "리타";
        nikkeData.Squad = "마이티 톨즈";
        nikkeData.Level = 215;
        nikkeData.TotalAttack = 12345;
        nikkeData.Hp = 1000000;
        nikkeData.Damage = 3123;
        nikkeData.Defence = 516;

    }


    public void OnNikkeClick()
    {
        UIManager.instance.OpenUI(Page.NIKKEINFO);
        nikkeInfo.Open(skeletonDataAsset, nikkeData);
    }


}
