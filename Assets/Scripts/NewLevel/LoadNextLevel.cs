using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [HideInInspector] public Vector2 respawnPos;


    bool levelSpawned = false;

    GameObject gameMaster;

    [SerializeField] AudioClip transistionSFX;

    private AudioSource audioSource;



    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        audioSource = gameMaster.GetComponent<AudioSource>();
    }

    private void Update()
    {
        gameMaster.GetComponent<GameMaster>().locSpawnPoint = respawnPos;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !levelSpawned)
        {
            levelSpawned = true;

            respawnPos = other.transform.localPosition;

 
            gameMaster.GetComponent<GameMaster>().playerYpos = other.gameObject.transform.position.y;
            gameMaster.GetComponent<GameMaster>().playerXpos = other.gameObject.transform.position.x;

            LoadLevel();
        }
    }

    void LoadLevel()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(transistionSFX);
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}

