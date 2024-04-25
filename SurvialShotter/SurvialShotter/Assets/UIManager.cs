using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textScore;
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
}
