using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource audioSource;

    GameObject gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    private void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = gameMaster.GetComponent<GameMaster>().musicVolume / 2;
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying) { audioSource.Play(); }
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
