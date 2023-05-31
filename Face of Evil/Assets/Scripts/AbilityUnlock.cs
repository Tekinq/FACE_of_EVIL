using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{

    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;

    public GameObject pickupEffect;

    public string unlockMessage;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;

                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }

            if (unlockDash)
            {
                player.canDash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
                PlayerPrefs.SetInt("BallUnlocked", 1);
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
                PlayerPrefs.SetInt("BombUnlocked", 1);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.gameObject, 4f);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);

        }
    }


}
