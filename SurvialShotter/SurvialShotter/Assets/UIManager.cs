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
    public UnityEngine.UI.Image imageGameOverBackground;
    public UnityEngine.UI.Image imagePlayerHit;
    public float backgroundDuration = 1f;
    public float backgroundTime = 0f;

    private int score { get; set; }

    private static UIManager m_instance; // �̱����� �Ҵ�� ����

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
    }

    // Update is called once per frame
    void Update()
    {
        
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

    IEnumerator CoPlayerHit()
    {
        yield return null;
    }

    IEnumerator CoGameOver()
    {
        while (imageGameOverBackground.color.a < 1f)
        {
            backgroundTime += Time.deltaTime;

            Debug.Log(imageGameOverBackground.color);
            imageGameOverBackground.color = Color.Lerp(new Color(0f, 0f, 0f, 0f),
                new Color(0.1f, 0.1f, 0.1f, 1f), backgroundTime / backgroundDuration * 2f);

            yield return null;
        }
            
        yield break;
    }
}