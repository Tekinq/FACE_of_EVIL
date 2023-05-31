using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{

    public static BossHealthController instance;

    public void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;

    public int currentHealth;

    public BossBattle theBoss;

    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            theBoss.EndBattle();

            AudioManager.instance.PlaySFX(0);

        }
        else
        {
            AudioManager.instance.PlaySFX(1);
        }

        bossHealthSlider.value = currentHealth;
    }
}
