using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOver : GernericWindow
{
    public TextMeshProUGUI leftStat;
    public TextMeshProUGUI leftScore;

    public TextMeshProUGUI rightStat;
    public TextMeshProUGUI rightScore;

    public TextMeshProUGUI totalScore;

    protected override void Awake()
    {
        
    }

    protected void Start()
    {
        // Open();
    }

    public override void Open()
    {
        leftStat.text = string.Empty;
        leftScore.text = string.Empty;
        rightStat.text = string.Empty;
        rightScore.text = string.Empty;

        if(gameObject.activeSelf)
            StartCoroutine(AddScore());

        base.Open();
    }

    public void OnClick()
    {

    }

    IEnumerator AddScore()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                int randomSocre = UnityEngine.Random.Range(0, 9999);
                if(i != 0)
                {
                    leftStat.text += Environment.NewLine + $"STAT";
                    leftScore.text += Environment.NewLine + $"{randomSocre}";
                }
                else
                {
                    leftStat.text += $"STAT";
                    leftScore.text += $"{randomSocre}";
                }

                yield return new WaitForSeconds(1f);
            }

            for (int i = 0; i < 3; i++)
            {
                int randomSocre = UnityEngine.Random.Range(0, 9999);
                if (i != 0)
                {
                    rightStat.text += Environment.NewLine + $"STAT";
                    rightScore.text += Environment.NewLine + $"{randomSocre}";
                }
                else
                {
                    rightStat.text += $"STAT";
                    rightScore.text += $"{randomSocre}";
                }

                yield return new WaitForSeconds(1f);
            }

            // totalScore
            {
                int randomSocre = UnityEngine.Random.Range(1, 99999999);
                float currentTime = 0;
                while (true)
                {

                    float c = Mathf.Lerp(0, randomSocre, currentTime / 3f);
                    int f = (int)c;
                    totalScore.text = f.ToString().PadLeft(8, '0');

                    currentTime += Time.deltaTime;

                    yield return null;

                    if (c >= randomSocre) yield break;
                }

            }            

        }

    }
}
