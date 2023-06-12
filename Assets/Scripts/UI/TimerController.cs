using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    [SerializeField] TextMeshProUGUI timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    GameObject gameMaster;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        timeCounter.text = "Time: 00:00:00.00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        
        while (timerGoing)
        {
            gameMaster.GetComponent<GameMaster>().timer += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(gameMaster.GetComponent<GameMaster>().timer);
            string timePlayingStr = timePlaying.ToString("hh':'mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
