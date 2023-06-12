using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlInfo : MonoBehaviour
{
    [HideInInspector] public Vector2 respawnForce;

    [SerializeField] bool needRespawnForce = false;

    [SerializeField] GameObject currentCamera;
    [SerializeField] GameObject currentLevel;


    void Start()
    {
        
    }

    void Update()
    {
        if (needRespawnForce)
        {
            respawnForce = new Vector2(50, 0);
        }
        else
        {
            respawnForce = new Vector2(0, 0);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCamera.GetComponent<Camera>().enabled = true;
            currentCamera.GetComponent<AudioListener>().enabled = true;
            currentLevel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentCamera.GetComponent<Camera>().enabled = false;
        currentCamera.GetComponent<AudioListener>().enabled = false;
        currentLevel.SetActive(false);
    }

}
