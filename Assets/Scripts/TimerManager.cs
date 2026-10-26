using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timerTime;
    public TextMeshProUGUI timerText;
    public TextMeshPro finalTimeText;
    public TextMeshPro bestTimeText;
    public RoomManager roomManager;
    public GameObject monster;
    public bool isPaused = true;
    public GameObject[] foodies;
    public UpgradeManager upgradeManager;
    private float bestTime = 0.0f;
    private bool recordBeat;
    public GameObject endNewBest;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time: " + timerTime.ToString("00:00.00");

        if (isPaused == false){
            timerTime -= Time.deltaTime;
            roomManager.timeToBeat += Time.deltaTime;
        }

        if (timerTime < 0.0f){
            timerTime = 0.0f;
            monster.SetActive(true);
        }
    }

    public void AddTime(float timeToAdd)
    {
        if(upgradeManager.upBonusTime)
        {
            timerTime += timeToAdd * 1.5f;
        }
        else
        {
           timerTime += timeToAdd; 
        }
    }

    public void BestTime(float finishTime)
    {
        endNewBest.SetActive(false);
        bestTime = PlayerPrefs.GetFloat("Best Time");
            
            if(bestTime == 0)
            {
                bestTime = finishTime;
                PlayerPrefs.SetFloat("Best Time", bestTime);
                recordBeat = true;
            }
            else
            {
                if (finishTime < bestTime)
                {
                    bestTime = finishTime;
                    PlayerPrefs.SetFloat("Best Time", bestTime);
                    recordBeat = true;
                }
            }

            finalTimeText.text = "You escaped in:\n" + finishTime.ToString("00:00.000");
            
            if(recordBeat)
            {
                endNewBest.SetActive(true);
                bestTimeText.gameObject.SetActive(false);
            }
            else
            {
                bestTimeText.gameObject.SetActive(true);
                bestTimeText.text = "Your Personal Best:\n" + bestTime.ToString("00:00.000");
            }

    }
}

