using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public int deaths = 0;

    public float timer;

    public float playerYpos;
    public float playerXpos;

    public float musicVolume;
    public float sfxVolume;

    public bool isOnQuickThinking = false;
    public bool isOnOutOfControl = false;
    public bool isOnNoGravity = false;
    public bool isGamemodesActivated = false;

    public bool isFullscreen = false;


    public bool isFirstLevel = true;
    public bool hasWon;
    int firstLevelNumber;

    public bool isPaused;

    public Vector2 locSpawnPoint;
    public Vector2 playerVelocity;
    public Quaternion playerRotation;

    private void Start()
    {
        firstLevelnumberCheck();
    }

    void Update()
    {
        DontDestroyOnLoad(this);

        IsFirstLevelCheck();
       

        if (isOnQuickThinking || isOnOutOfControl || isOnNoGravity)
        {
            isGamemodesActivated = true;
        } else { isGamemodesActivated = false; }

        gameObject.GetComponent<AudioSource>().volume = sfxVolume;

        if (isFullscreen) { Screen.fullScreen = true; }
        else { Screen.fullScreen = false; }
    }

    void firstLevelnumberCheck()
    {
        firstLevelNumber = SceneManager.GetActiveScene().buildIndex;

        if (PlayerPrefs.GetInt("hasWon") == 1)
        {
            hasWon = true;
        }
    }

    void IsFirstLevelCheck()
    {
        int currentLevelNumber = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelNumber != firstLevelNumber) { isFirstLevel = false; }
    }
}
