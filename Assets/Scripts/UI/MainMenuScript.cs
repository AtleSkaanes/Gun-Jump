using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button playerCustButton;
    [SerializeField] Button quitGameButton;
    Button startBtn;
    Button pcBtn;
    Button quitBtn;

    [Header("Toggles")]
    public Toggle NoGravityToggle;
    public Toggle OutOfControlToggle;
    public Toggle QuickThinkingToggle;

    [Header("Highscore")]
    public TextMeshProUGUI highDeathTXT;
    public TextMeshProUGUI highDeathTimerTXT;

    public TextMeshProUGUI highTimerTXT;
    public TextMeshProUGUI highTimerDeathTXT;

    TimeSpan timespanTimerHigh;
    TimeSpan timespanDeathHigh;

    [Header("Warning objects")]
    [SerializeField] Button okButton;
    [SerializeField] Button cancelButton;
    Button okBtn;
    Button cancelBtn;

    [SerializeField] GameObject Warning;
    Animator warningAnimator;



    bool isTransistioning = false;

    bool isOnNoGravity = false;
    bool isOnOutOfControl = false;
    bool isOnQuickThinking = false;

    GameObject gameMaster;
        

    void Start()
    {
        startBtn = startButton.GetComponent<Button>();
        pcBtn = playerCustButton.GetComponent<Button>();
        quitBtn = quitGameButton.GetComponent<Button>();

        okBtn = okButton.GetComponent<Button>();
        cancelBtn = cancelButton.GetComponent<Button>();

        warningAnimator = Warning.GetComponent<Animator>();

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");

        CheckIfHasWon();
    }


    void Update()
    {   
        CheckIfToggled();

        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);

        okBtn.onClick.AddListener(GoToFirstLevel);
        cancelBtn.onClick.AddListener(RemoveWarning);

        UpdateHighScore();
    }

    void CheckIfHasWon()
    {
        if (PlayerPrefs.GetInt("hasWon") != 1)
        {
            NoGravityToggle.gameObject.SetActive(false);
            OutOfControlToggle.gameObject.SetActive(false);
            QuickThinkingToggle.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("hasWon") == 1)
        {
            NoGravityToggle.gameObject.SetActive(true);
            OutOfControlToggle.gameObject.SetActive(true);
            QuickThinkingToggle.gameObject.SetActive(true);
        }
    }

    void CheckIfToggled()
    {
        if (NoGravityToggle.GetComponent<Toggle>().isOn)
        {
            isOnNoGravity = true;
        }
        else { isOnNoGravity = false; }
        
        if (OutOfControlToggle.GetComponent<Toggle>().isOn)
        {
            isOnOutOfControl = true;
        }
        else { isOnOutOfControl = false; }
        
        if (QuickThinkingToggle.GetComponent<Toggle>().isOn)
        {
            isOnQuickThinking = true;
        }
        else { isOnQuickThinking = false; }
    }

    public void StartGame()
    {

        if (isOnNoGravity) { gameMaster.GetComponent<GameMaster>().isOnNoGravity = true; }
        else { gameMaster.GetComponent<GameMaster>().isOnNoGravity = false; }

        if (isOnOutOfControl) { gameMaster.GetComponent<GameMaster>().isOnOutOfControl = true; }
        else { gameMaster.GetComponent<GameMaster>().isOnOutOfControl = false; }

        if (isOnQuickThinking) { gameMaster.GetComponent<GameMaster>().isOnQuickThinking = true; }
        else { gameMaster.GetComponent<GameMaster>().isOnQuickThinking = false; }

        if (isOnNoGravity || isOnOutOfControl || isOnQuickThinking)
        {
            Warning.gameObject.SetActive(true);

            warningAnimator.SetTrigger("RollIn");
            warningAnimator.ResetTrigger("RollOut");
        } else { GoToFirstLevel(); }
    }

    void QuitGame()
    {
        Application.Quit();
    }


    void GoToFirstLevel()
    {
        if (!isTransistioning)
        {
            SceneManager.LoadScene(1);
            isTransistioning = true;
        }
    }

    void RemoveWarning()
    {
        Warning.gameObject.SetActive(false);

        warningAnimator.SetTrigger("RollOut");
        warningAnimator.ResetTrigger("RollIn");

        NoGravityToggle.isOn = false;
        OutOfControlToggle.isOn = false;
        QuickThinkingToggle.isOn = false;
    }

    void UpdateHighScore()
    {
        timespanTimerHigh = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("highTimer"));
        string timePlayingHigh = timespanTimerHigh.ToString("hh':'mm':'ss'.'ff");
        timespanDeathHigh = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("highDeathTimer"));
        string timePlayingHighDeath = timespanDeathHigh.ToString("hh':'mm':'ss'.'ff");

        highDeathTXT.text = PlayerPrefs.GetInt("highDeath").ToString();
        highTimerDeathTXT.text = PlayerPrefs.GetInt("highTimerDeath").ToString();
        highTimerTXT.text = timePlayingHigh;
        highDeathTimerTXT.text = timePlayingHighDeath;
    }
}
