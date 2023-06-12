using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float respawnTimer;
    float deathCounter;

    Vector2 respawnForce;

    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isRespawning = false;
    [SerializeField] public bool needRespawnForce = false;

    [SerializeField] ParticleSystem deathParticles;
    GameObject child1;
    GameObject child2;
    GameObject child3;
    GameObject gameMaster;

    [SerializeField] public Transform respawnPoint;

    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] AudioClip deathSFX;

    Rigidbody2D rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        child1 = gameObject.transform.GetChild(0).gameObject;
        child2 = gameObject.transform.GetChild(1).gameObject;
        child3 = gameObject.transform.GetChild(2).gameObject;

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");

        ForceOnFirstLevel();

    }

    // Update is called once per frame
    void Update()
    {
        DeathCounterUpdate();
        CheckRespawnButton();
        CheckRespawnForce();
        UpdateVaribles();
    }

    private void CheckRespawnForce()
    {
        if (needRespawnForce)
        {
            respawnForce = new Vector2(50, 0);
        }
        else if (!needRespawnForce && gameMaster.GetComponent<GameMaster>().isOnNoGravity)
        {
            respawnForce = new Vector2(0, -30);
        }
        else
        {
            respawnForce = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead == false)
        {
            

            gameMaster.GetComponent<GameMaster>().deaths += 1;

            child1.GetComponent<SpriteRenderer>().enabled = false;
            child2.GetComponent<SpriteRenderer>().enabled = false;
            child3.GetComponent<SpriteRenderer>().enabled = false;

            rb.simulated = false;
            rb.velocity = new Vector2(0, 0);

            deathParticles.Play();

            audioSource.PlayOneShot(deathSFX);

            Invoke("Respawn", respawnTimer);

            isDead = true;
        }
    }

    void Respawn()
    {
        isRespawning = true;

        isDead = false;

        child1.GetComponent<SpriteRenderer>().enabled = true;
        child2.GetComponent<SpriteRenderer>().enabled = true;
        child3.GetComponent<SpriteRenderer>().enabled = true;

        rb.simulated = true;

        transform.position = respawnPoint.position;

        rb.AddForce(respawnForce);

        isRespawning = false;
    }

    void DeathCounterUpdate()
    {
        deathText.text = deathCounter.ToString("000");
    }

    void CheckRespawnButton()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Die();
        }
    }

    void UpdateVaribles()
    {
        deathCounter = gameMaster.GetComponent<GameMaster>().deaths;

        gameObject.GetComponent<AudioSource>().volume = gameMaster.GetComponent<GameMaster>().sfxVolume;
    }

    void ForceOnFirstLevel()
    {
        if (needRespawnForce && gameMaster.GetComponent<GameMaster>().isFirstLevel)
        {
            rb.AddForce(respawnForce);
        }
        if(gameMaster.GetComponent<GameMaster>().isFirstLevel)
        {
            transform.position = respawnPoint.position;
        }
    }
}
