using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totolHealth = 3;

    public GameObject deathEffect;

    public void DamageEnemy( int damageAmount)
    {
        totolHealth -= damageAmount;

        if(totolHealth <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect,transform.position,transform.rotation);
            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(4);
        }
    }
}
