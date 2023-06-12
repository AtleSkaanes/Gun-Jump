using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float cooldown = 5f;
    [SerializeField] float cooldownStatus = 0f;
    [SerializeField] float moveSpeed;
    [SerializeField] float slowmo = 0.5f;
    float dragLineLength;
    float fixedDeltaTime;

    bool dragLineSpawned = false;
    bool isDead;
    bool isPaused;

    public bool isOnQuickThinking;
    public bool isOnOutOfControl;
    public bool isOnNoGravity;


    [SerializeField] ParticleSystem ShootParticles;
    GameObject newDragLine;
    GameObject gameMaster;
    [SerializeField] GameObject dragLineObject;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] GameObject bulletObject;
    [SerializeField] Transform barrrelEnd;
    [SerializeField] Slider cooldownBar;


    private Rigidbody2D rb;
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");

        this.fixedDeltaTime = Time.fixedDeltaTime;

        
    }

    // Update is called once per frame
    void Update()
    {
        IfClick();
        DepleteCooldown();
        //MaxVelocityCheck();

        isDead = GetComponent<PlayerDeath>().isDead;
        isPaused = gameMaster.GetComponent<GameMaster>().isPaused;

        CheckGameMode();

        gameMaster.GetComponent<GameMaster>().playerVelocity = rb.velocity;
        gameMaster.GetComponent<GameMaster>().playerRotation = transform.rotation;

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
    }

    void IfClick()
    {
        if (isPaused) { return; }

        if (Input.GetMouseButton(0) && !isDead)
        {
            SlowMoDrag();
        }

        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            Destroy(newDragLine);
            dragLineSpawned = false;
        }

        if (Input.GetMouseButtonUp(0) && cooldownStatus <= Mathf.Epsilon && !isDead)
        {
            Shoot();
            ShootParticles.Play();
            cooldownStatus += cooldown;
        }
    }

    void SlowMoDrag()
    {
        if (isPaused) { return; }

        if (!isOnQuickThinking)
        {
            Time.timeScale = slowmo;
        }
        

        if (!dragLineSpawned)
        {
            newDragLine = Instantiate(dragLineObject, transform.position, transform.rotation);
            dragLineSpawned = true;
        }

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        dragLineLength = Mathf.Sqrt(Mathf.Pow(worldMousePos.x - transform.position.x, 2) + Mathf.Pow(worldMousePos.y - transform.position.y, 2));

        float midpointX = (worldMousePos.x + transform.position.x) / 2;
        float midpointY = (worldMousePos.y + transform.position.y) / 2;

        newDragLine.transform.localScale = new Vector2(dragLineLength, 1);
        newDragLine.transform.rotation = Quaternion.Euler(0, 0, angle);
        newDragLine.transform.position = new Vector2(midpointX, midpointY);
    }

    void Shoot()
    {
        gameMaster.GetComponent<GameMaster>().isFirstLevel = false;

        if (isPaused) { return; }

        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.y = mousePos.y - objectPos.y;

        rb.AddForce(-transform.right * dragLineLength * moveSpeed); // Move

        Instantiate(bulletObject, barrrelEnd.position, barrrelEnd.rotation); // shoot bullet

        TriggerShootSound();
    }

    void TriggerShootSound()
    {
        audioSource.PlayOneShot(shootSFX);
    }

    void DepleteCooldown()
    {
        //if (isPaused) { return; }

        cooldownBar.maxValue = cooldown;
        if (cooldownStatus >= Mathf.Epsilon)
        {
            cooldownStatus = cooldownStatus - 1f * Time.deltaTime;
        }
        cooldownBar.value = cooldownStatus;
        if (isDead)
        {
            cooldownStatus = Mathf.Epsilon;
        }
    }

    void CheckGameMode()
    {
        isOnQuickThinking = gameMaster.GetComponent<GameMaster>().isOnQuickThinking;
        isOnOutOfControl = gameMaster.GetComponent<GameMaster>().isOnOutOfControl;
        isOnNoGravity = gameMaster.GetComponent<GameMaster>().isOnNoGravity;

        if (isOnNoGravity)
        {
            Physics2D.gravity = new Vector2(0, 0);
        }
        if (isOnOutOfControl)
        {
            moveSpeed = 100f;
        }
    }
}
