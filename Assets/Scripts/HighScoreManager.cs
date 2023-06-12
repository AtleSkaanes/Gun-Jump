using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public int deaths;
    public int highDeaths = 10000;
    public bool newDeathHigh = false;

    public float timer;
    public float highTimer = 10000;
    public bool newTimerHigh = false;

    TimeSpan timespanCurrent;
    TimeSpan timespanHigh;

    [Header("Text")]
    public TextMeshProUGUI deathTXT;
    public TextMeshProUGUI highDeathTXT;
    public TextMeshProUGUI newDeathHighTXT;

    public TextMeshProUGUI timerTXT;
    public TextMeshProUGUI highTimerTXT;
    public TextMeshProUGUI newTimerHighTXT;

    [Header("Buttons")]
    [SerializeField] Button MainMenuButton;
    [SerializeField] Button quitGameButton;

    bool isGoingToMainMenu = false;

    GameObject gameMaster;

    Button MMBtn;
    Button quitBtn;

    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        UpdateVariables();
        //UpdateHighScore();

        MMBtn = MainMenuButton.GetComponent<Button>();
        quitBtn = quitGameButton.GetComponent<Button>();

    }


    void Update()
    {
        UpdateText();

        MMBtn.onClick.AddListener(GoToMainMenu);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void UpdateVariables()
    {
        deaths = gameMaster.GetComponent<GameMaster>().deaths;
        timer = gameMaster.GetComponent<GameMaster>().timer;
    }

    void UpdateHighScore()
    {
        if (deaths < highDeaths)
        {
            PlayerPrefs.SetInt("HighDeaths", deaths);
            //newDeathHighTXT.GetComponent<TextMeshPro>().enabled = true;
            newDeathHighTXT.enabled = true;
        }
        if (timer < highTimer)
        {
            PlayerPrefs.SetFloat("HighTimer", timer);
            //newTimerHighTXT.GetComponent<TextMeshPro>().enabled = true;
            newTimerHighTXT.enabled = true;
        }

        highDeaths = PlayerPrefs.GetInt("HighDeaths");
        highTimer = PlayerPrefs.GetFloat("HighTimer");
        PlayerPrefs.Save();


        Debug.Log(PlayerPrefs.GetInt("HighDeaths"));
        Debug.Log(PlayerPrefs.GetFloat("HighTimer"));
    }

    void UpdateText()
    {
        timespanCurrent = TimeSpan.FromSeconds(timer);
        string timePlayingCurrent = timespanCurrent.ToString("hh':'mm':'ss'.'ff");
        timespanHigh = TimeSpan.FromSeconds(highTimer);
        string timePlayingHigh = timespanCurrent.ToString("hh':'mm':'ss'.'ff");

        highDeathTXT.text = highDeaths.ToString();
        highTimerTXT.text = timePlayingHigh;
        deathTXT.text = deaths.ToString();
        timerTXT.text =timePlayingCurrent;
    }

    void GoToMainMenu()
    {
        if (!isGoingToMainMenu)
        {
            Destroy(gameMaster.gameObject);
            Destroy(GameObject.FindGameObjectWithTag("bgMusic"));

            SceneManager.LoadScene(0);
            isGoingToMainMenu = true;
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
