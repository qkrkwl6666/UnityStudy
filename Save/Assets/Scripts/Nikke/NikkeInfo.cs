using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
using Spine.Unity;
using System.Linq;
using UnityEditor.UI;
using Spine.Unity.Editor;

public class NikkeInfo : MonoBehaviour
{
    public TextMeshProUGUI nikkeName;
    public Image bust;
    public TextMeshProUGUI totalAttack;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI defence;

    public GameObject NikkeInforightUI;
    public GameObject ViewMode;
    public List<SkeletonDataAsset> skeletonDataAssets;
    public SkeletonAnimation skeletonAnimation;

    public Button backButton;

    public GameObject live2D;

    private bool isViewMode = false;

    private bool isAiming = false;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isViewMode)
        {
            skeletonAnimation.AnimationName = "action";
            skeletonAnimation.loop = false;
        }

        CoverToAim();

        if(isAiming && skeletonAnimation.AnimationName == "aim_idle")
        {
            skeletonAnimation.loop = true;
            skeletonAnimation.AnimationName = "aim_fire";
        }

        if(isAiming && skeletonAnimation.AnimationName == "aim_fire" && Input.GetMouseButtonUp(0))
        {
            skeletonAnimation.loop = false;
            skeletonAnimation.AnimationName = "to_cover";
        }
    }

    private void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        ViewMode.GetComponent<Button>().onClick.AddListener(ViewModeOpenUI);
        backButton.onClick.AddListener(OnBackButtonClick);
    }


    public void Open(List<SkeletonDataAsset> _skeletonDataAssets, NikkeData data)
    {
        isAiming = false;
        isViewMode = false;
        NikkeInforightUI.SetActive(true);
        live2D.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
        skeletonDataAssets = _skeletonDataAssets;
        nikkeName.text = data.name;
        totalAttack.text = data.TotalAttack.ToString();
        hp.text = data.Hp.ToString();
        damage.text = data.Damage.ToString();
        defence.text = data.Defence.ToString();

        skeletonAnimation.skeletonDataAsset = skeletonDataAssets[(int)Status.IDLE];
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;

        skeletonAnimation.AnimationName = "idle";
        SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(skeletonAnimation);
    }
    private void OnAnimationComplete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "action")
        {
            skeletonAnimation.loop = true;
            skeletonAnimation.AnimationName = "idle";
        }
        else if (trackEntry.Animation.Name == "to_aim")
        {
            skeletonAnimation.loop = true;
            skeletonAnimation.AnimationName = "aim_idle";
        }

        else if (trackEntry.Animation.Name == "to_cover")
        {
            isAiming = false;
            skeletonAnimation.loop = false;
            skeletonAnimation.skeletonDataAsset = skeletonDataAssets[(int)Status.COVER];
            skeletonAnimation.AnimationName = "cover_reload";
            SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(skeletonAnimation);
        }

        else if (trackEntry.Animation.Name == "cover_reload")
        {
            skeletonAnimation.loop = true;
            skeletonAnimation.AnimationName = "cover_idle";
        }
    }

    public void ViewModeOpenUI()
    {
        isViewMode = true;
        NikkeInforightUI.SetActive(false);
        ViewMode.SetActive(true);

        skeletonAnimation.skeletonDataAsset = skeletonDataAssets[(int)Status.COVER];

        skeletonAnimation.loop = true;
        skeletonAnimation.AnimationName = "cover_idle";

        // ¸®·Îµå
        SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(skeletonAnimation);

        live2D.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150, 100);
    }

    public void OnBackButtonClick()
    {
        UIManager.instance.OpenUI(Page.NIKKELIST);
    }

    private void CoverToAim()
    {
        if (Input.GetMouseButtonDown(0) && isViewMode && !isAiming)
        {
            skeletonAnimation.loop = false;
            skeletonAnimation.AnimationName = "to_aim";
            skeletonAnimation.skeletonDataAsset = skeletonDataAssets[(int)Status.AIM];
            SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(skeletonAnimation);
            isAiming = true;
        }
    }


}
