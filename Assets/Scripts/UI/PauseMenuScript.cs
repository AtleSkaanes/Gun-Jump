using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuScript : MonoBehaviour
{
    bool isPaused = false;
    bool isGoingToMainMenu = false;
    public bool isFirstLevel;

    [SerializeField] Button resumeButton;
    [SerializeField] Button MainMenuButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] TextMeshProUGUI sfxText;

    [Header("Highscore")]
    public TextMeshProUGUI highDeathTXT;
    public TextMeshProUGUI highDeathTimerTXT;

    public TextMeshProUGUI highTimerTXT;
    public TextMeshProUGUI highTimerDeathTXT;

    [Header("Warning objects")]
    [SerializeField] Button okButton;
    [SerializeField] Button cancelButton;
    Button okBtn;
    Button cancelBtn;

    [SerializeField] GameObject Warning;
    [SerializeField] Transform Parent;
    Animator warningAnimator;
    bool isQuttingToMainMenu = false;


    //Misc
    TimeSpan timespanTimerHigh;
    TimeSpan timespanDeathHigh;


    GameObject gameMaster;

    Animator animator;

    Button resBtn;
    Button MMBtn;
    Button quitBtn;

    void Start()
    {
        resBtn = resumeButton.GetComponent<Button>();
        MMBtn = MainMenuButton.GetComponent<Button>();
        quitBtn = quitGameButton.GetComponent<Button>();

        okBtn = okButton.GetComponent<Button>();
        cancelBtn = cancelButton.GetComponent<Button>();

        warningAnimator = Warning.GetComponent<Animator>();

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        animator = gameObject.GetComponent<Animator>();

        LoadVariables();
    }

    
    void Update()
    {
        ActivatePauseMenu();
        UpdateVariables();
        UpdateText();
        //CheckIfFullScreen();
        resBtn.onClick.AddListener(ResumeGame);
        MMBtn.onClick.AddListener(MainMenuWarning);
        quitBtn.onClick.AddListener(QuitWarning);

        if(isQuttingToMainMenu) { okBtn.onClick.AddListener(GoToMainMenu); }
        else if (!isQuttingToMainMenu) { okBtn.onClick.AddListener(QuitGame); }

        cancelBtn.onClick.AddListener(RemoveWarning);

        UpdateHighScore();

        gameMaster.GetComponent<GameMaster>().isPaused = isPaused;
    }

    void ActivatePauseMenu()
    {
        if (Input.GetButtonDown("Cancel") && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetButtonDown("Cancel") && isPaused)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;


        animator.SetTrigger("RollIn");
        animator.ResetTrigger("RollOut");
    }

    void ResumeGame()
    {
        isPaused = false;


        animator.SetTrigger("RollOut");
        animator.ResetTrigger("RollIn");
    }

    void CheckIfFullScreen()
    {
        if (fullScreenToggle.GetComponent<Toggle>().isOn)
        {
            gameMaster.GetComponent<GameMaster>().isFullscreen = true;
        }
        else { gameMaster.GetComponent<GameMaster>().isFullscreen = false; }
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

    void MainMenuWarning()
    {
        isQuttingToMainMenu = true;
        AddWarning();
    }

    void QuitWarning()
    {
        isQuttingToMainMenu = false;
        AddWarning();
    }


    void AddWarning()
    {
        Warning.SetActive(true);
    }
    
    void RemoveWarning()
    {
        warningAnimator.SetTrigger("RollOut");
        Warning.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void UpdateText()
    {
        musicText.text = (musicSlider.value*10).ToString("f0");
        sfxText.text = (sfxSlider.value*10).ToString("f0");
    }

    void LoadVariables()
    {
        isFirstLevel = gameMaster.GetComponent<GameMaster>().isFirstLevel;

        musicSlider.value = gameMaster.GetComponent<GameMaster>().musicVolume;
        sfxSlider.value = gameMaster.GetComponent<GameMaster>().sfxVolume;

        if (gameMaster.GetComponent<GameMaster>().isFirstLevel)
        {
            musicSlider.value = 0.5f;
            sfxSlider.value = 0.5f;
        }
    }

    void UpdateVariables()
    {
        gameMaster.GetComponent<GameMaster>().musicVolume = musicSlider.value;
        gameMaster.GetComponent<GameMaster>().sfxVolume = sfxSlider.value;

        PlayerPrefs.SetFloat("musicSlider", musicSlider.value);
        PlayerPrefs.SetFloat("sfxSlider", sfxSlider.value);
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
