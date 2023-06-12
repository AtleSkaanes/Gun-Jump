using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    float movementFactor;
    [SerializeField] Vector2 moveToLocation;
    Vector2 startingLocation;

    [SerializeField] bool resetOnSpawn;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        startingLocation = gameObject.transform.position;
    }

    void Update()
    { 
        if (moveSpeed <= Mathf.Epsilon) { return; } //protection against NaN

        float cycles = Time.time / moveSpeed;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector2 offset = moveToLocation * movementFactor;
        transform.position = startingLocation + offset;
        
        //resetOnSpawn == true &&
        //player.GetComponent<PlayerDeath>().isDead == true

        if (resetOnSpawn && player.GetComponent<PlayerDeath>().isDead)
        {
                        
        }
    }
}
