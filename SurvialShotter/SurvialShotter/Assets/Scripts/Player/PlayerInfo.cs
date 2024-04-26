using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInfo : CreatureInfo
{
    private int startHp = 100;
    private Animator animator;
    private UnityEngine.UI.Slider hpSlider;

    public override void OnDamege(int damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if(isDead) return;

        hp -= damage;
        hpSlider.value = hp;

        UIManager.instance.PlayerHitEffect();

        if (hp < 0 && !isDead)
        {
            Die();
        }
    }
    private void OnEnable()
    {
        hp = startHp;
        hpSlider.value = hp;
        isDead = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hpSlider = GameObject.FindWithTag("PlayerSlider").GetComponent<UnityEngine.UI.Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.minValue = minHp;
        hpSlider.maxValue = startHp;
        hp = startHp;
        hpSlider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        isDead = true;
        hp = 0;
        hpSlider.value = 0;
        animator.SetTrigger("Death");
        UIManager.instance.GameOverUI();
        //StopCoroutine("EnemySpawner"); 
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0 , LoadSceneMode.Single);
    }


}
