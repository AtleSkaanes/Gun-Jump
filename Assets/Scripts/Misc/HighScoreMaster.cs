using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HighScoreMaster : MonoBehaviour
{
    public int deaths;

    public float timer;

    TimeSpan timespanCurrent;
    TimeSpan timespanTimerHigh;
    TimeSpan timespanDeathHigh;

    GameObject gameMaster;

    [Header("text")]
    public TextMeshProUGUI deathTXT;
    public TextMeshProUGUI highDeathTXT;
    public TextMeshProUGUI highDeathTimerTXT;
    public TextMeshProUGUI newDeathHighTXT;

    public TextMeshProUGUI timerTXT;
    public TextMeshProUGUI highTimerTXT;
    public TextMeshProUGUI highTimerDeathTXT;
    public TextMeshProUGUI newTimerHighTXT;

    [Header("Buttons")]
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] Button resetScoreButton;
    Button MMBtn;
    Button quitBtn;
    Button RSBtn;

    bool isGoingToMainMenu = false;

    void Start()
    {
        PlayerPrefs.SetInt("hasWon", 1);

        MMBtn = mainMenuButton.GetComponent<Button>();
        quitBtn = quitGameButton.GetComponent<Button>();
        RSBtn = resetScoreButton.GetComponent<Button>();

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
    }

    
    void Update()
    {
        deaths = gameMaster.GetComponent<GameMaster>().deaths;
        timer = gameMaster.GetComponent<GameMaster>().timer;

        UpdateText();
        MMBtn.onClick.AddListener(GoToMainMenu);
        quitBtn.onClick.AddListener(QuitGame);
        RSBtn.onClick.AddListener(ResetScore);
        CheckHighScore();
    }

    public void UpdateText()
    {
        timespanCurrent = TimeSpan.FromSeconds(timer);
        string timePlayingCurrent = timespanCurrent.ToString("hh':'mm':'ss'.'ff");

        timespanTimerHigh = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("highTimer"));
        string timePlayingHigh = timespanTimerHigh.ToString("hh':'mm':'ss'.'ff");
        timespanDeathHigh = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("highDeathTimer"));
        string timePlayingHighDeath = timespanDeathHigh.ToString("hh':'mm':'ss'.'ff");

        highDeathTXT.text = PlayerPrefs.GetInt("highDeath").ToString();
        highTimerDeathTXT.text = PlayerPrefs.GetInt("highTimerDeath").ToString();
        highTimerTXT.text = timePlayingHigh;
        highDeathTimerTXT.text = timePlayingHighDeath;
        deathTXT.text = deaths.ToString();
        timerTXT.text = timePlayingCurrent;

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

    void CheckHighScore()
    {
        if (deaths < PlayerPrefs.GetInt("highDeath") || (deaths == PlayerPrefs.GetInt("highDeath") && timer < PlayerPrefs.GetFloat("highDeathTimer")) && !gameMaster.GetComponent<GameMaster>().isGamemodesActivated)
        {
            PlayerPrefs.SetInt("highDeath", deaths);
            PlayerPrefs.SetFloat("highDeathTimer", timer);
            newDeathHighTXT.enabled = true;

        }
        if (timer < PlayerPrefs.GetFloat("highTimer") || (timer == PlayerPrefs.GetFloat("highTimer") && deaths < PlayerPrefs.GetInt("highTimerDeath")) && !gameMaster.GetComponent<GameMaster>().isGamemodesActivated)
        {
            PlayerPrefs.SetFloat("highTimer", timer);
            PlayerPrefs.SetInt("highTimerDeath", deaths);
            newTimerHighTXT.enabled = true;
        }
        
    }

    void ResetScore()
    {
        PlayerPrefs.SetInt("highDeath", 10000);
        PlayerPrefs.SetFloat("highTimer", 10000f);
    }
}
