using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timerTime;
    public TextMeshProUGUI timerText;
    public RoomManager roomManager;
    public GameObject monster;
    public bool isPaused = true;
    public GameObject[] foodies;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time: " + timerTime.ToString("00:00.00");

        if (isPaused == false){
            timerTime -= Time.deltaTime;
        }

        if (timerTime < 0.0f){
            timerTime = 0.0f;
            monster.SetActive(true);
        }
    }

    public void AddTime(float timeToAdd)
    {
        timerTime += timeToAdd;
    }
}

