using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textScore;
    public TMPro.TextMeshProUGUI textGameOver;
    public UnityEngine.UI.Image Option;
    public UnityEngine.UI.Image imageGameOverBackground;
    public UnityEngine.UI.Image imagePlayerHit;
    public float backgroundDuration = 1f;
    public float backgroundTime = 0f;

    private int score { get; set; }

    private static UIManager m_instance; // ΩÃ±€≈Ê¿Ã «“¥Áµ… ∫Øºˆ

    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        imageGameOverBackground.transform.SetAsFirstSibling();
        Option.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Option");
            ShowOptionUI(Option.gameObject.activeSelf ? true : false);
        }
    }

    public void ShowOptionUI(bool active)
    {
        if (active)
        {
            Option.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Option.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        textScore.text = $"Score : {this.score}";
    }

    public void GameOverUI()
    {
        textGameOver.gameObject.SetActive(true);
        imageGameOverBackground.gameObject.SetActive(true);

        backgroundTime = 0;
        imageGameOverBackground.color = Color.clear;

        StartCoroutine(CoGameOver());
    }

    public void PlayerHitEffect()
    {
        StartCoroutine(CoPlayerHit());
    }

    IEnumerator CoPlayerHit()
    {
        while(true)
        {
            imagePlayerHit.color = Color.clear;

            float hitDuration = 0.2f;
            float hitTime = 0f;

            while (imagePlayerHit.color.a < 0.3f)
            {
                hitTime += Time.deltaTime;

                imagePlayerHit.color = Color.Lerp(new Color(0f, 0f, 0f, 0f),
                   new Color(1f, 0f, 0f, 0.3f), hitTime / hitDuration * 2f);
                yield return null;
            }

            hitTime = 0f;

            while (imagePlayerHit.color.a > 0.0f)
            {
                hitTime += Time.deltaTime;

                imagePlayerHit.color = Color.Lerp(new Color(1f, 0f, 0f, 0.3f),
                   new Color(0f, 0f, 0f, 0.0f), hitTime / hitDuration * 2f);
                yield return null;
            }
            yield break;
        }
    }

    IEnumerator CoGameOver()
    {
        while (imageGameOverBackground.color.a < 1f)
        {
            backgroundTime += Time.deltaTime;

            //Debug.Log(imageGameOverBackground.color);
            imageGameOverBackground.color = Color.Lerp(new Color(0f, 0f, 0f, 0f),
                new Color(0.1f, 0.1f, 0.1f, 1f), backgroundTime / backgroundDuration * 2f);

            yield return null;
        }
            
        yield break;
    }
}
