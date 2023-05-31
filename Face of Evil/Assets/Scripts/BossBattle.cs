using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{

    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;

    public Transform theBoss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;

        AudioManager.instance.PlayBossMusic();
    }


    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        if (BossHealthController.instance.currentHealth > threshold1)
        {
            if (activeCounter > 0)
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("vanish");
                }

                shotCounter -= Time.deltaTime;
                if (shotCounter <=0){
                    shotCounter = timeBetweenShots1;

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }
            }
            else if (fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if (fadeCounter <= 0)
                {
                    theBoss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;
                if (inactiveCounter <= 0)
                {
                    theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    theBoss.gameObject.SetActive(true);

                    activeCounter = activeTime;

                    shotCounter = timeBetweenShots1; 
                }
            }
        }
        else
        {
            if (targetPoint == null)
            {
                targetPoint = theBoss;
                fadeCounter = fadeoutTime;
                anim.SetTrigger("vanish");
            }
            else
            {
                if (Vector3.Distance(theBoss.position,targetPoint.position)>.02f)
                {
                    theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("vanish");
                    }

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        if (PlayerHealthController.instance.currentHealth > threshold2)
                        {
                            shotCounter = timeBetweenShots1;
                        }
                        else
                        {
                            shotCounter = timeBetweenShots2;
                        }

                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        int whileBreaker = 0;
                        while (targetPoint.position == theBoss.position&& whileBreaker<100)
                        {
                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            whileBreaker++;
                        }

                        theBoss.gameObject.SetActive(true);

                        if (PlayerHealthController.instance.currentHealth > timeBetweenShots2)
                        {
                            shotCounter = timeBetweenShots1;
                        }
                        else
                        {
                            shotCounter = timeBetweenShots2;
                        }

                    }
                }
            }
        }
    }

        public void EndBattle()
        {
            gameObject.SetActive(false);
        }
    
}
