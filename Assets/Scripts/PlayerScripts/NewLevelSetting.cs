using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewLevelSetting : MonoBehaviour
{
    [HideInInspector] int currentSceneIndex;
    float yPos;
    float xPos;

    bool isHorizontalSpawn;
    [SerializeField] bool needPositioning = true;

    [SerializeField] TextMeshProUGUI lvlText;
    Transform spawnPoint;
    GameObject gameMaster;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        spawnPoint = GetComponent<PlayerDeath>().respawnPoint;

        isHorizontalSpawn = GetComponent<PlayerDeath>().needRespawnForce;

        Spawn();
    }

    private void Update()
    {
        UpdateLevelCounter();
    }

    void Spawn()
    {
        yPos = gameMaster.GetComponent<GameMaster>().playerYpos;
        xPos = gameMaster.GetComponent<GameMaster>().playerXpos;
        rb.velocity = gameMaster.GetComponent<GameMaster>().playerVelocity;
        transform.rotation = gameMaster.GetComponent<GameMaster>().playerRotation;

        if (isHorizontalSpawn && needPositioning) { transform.position = new Vector2(spawnPoint.position.x, yPos); }
        if (!isHorizontalSpawn && needPositioning) { transform.position = new Vector2(xPos, spawnPoint.position.y); }
    }

    void UpdateLevelCounter()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        lvlText.text = currentSceneIndex.ToString("000");
    }

}
