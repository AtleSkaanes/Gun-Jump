using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointRef : MonoBehaviour
{
    int lvlNumber;
    [HideInInspector] public Vector2 checkpoint;
    [SerializeField] Transform lvl_1;
    [SerializeField] Transform lvl_2;
    [SerializeField] Transform lvl_3;
    [SerializeField] Transform lvl_4;
    [SerializeField] Transform lvl_5;
    [SerializeField] Transform lvl_6;
    [SerializeField] Transform lvl_7;
    [SerializeField] Transform lvl_8;
    [SerializeField] Transform lvl_9;
    [SerializeField] Transform lvl_10;
    [SerializeField] Transform lvl_11;
    [SerializeField] Transform lvl_12;
    [SerializeField] Transform lvl_13;
    [SerializeField] Transform lvl_14;
    [SerializeField] Transform lvl_15;
    [SerializeField] Transform lvl_16;
    [SerializeField] Transform lvl_17;
    [SerializeField] Transform lvl_18;
    [SerializeField] Transform lvl_19;
    [SerializeField] Transform lvl_20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCheckpoint();
        lvlNumber = 1;
    }

    void CheckForCheckpoint()
    {
        if (lvlNumber == 1) { checkpoint = lvl_1.position; }

        if (lvlNumber == 2) { checkpoint = lvl_2.position; }

        if (lvlNumber == 3) { checkpoint = lvl_3.position; }

        if (lvlNumber == 4) { checkpoint = lvl_4.position; }

        if (lvlNumber == 5) { checkpoint = lvl_5.position; }

        if (lvlNumber == 6) { checkpoint = lvl_6.position; }

        if (lvlNumber == 7) { checkpoint = lvl_7.position; }

        if (lvlNumber == 8) { checkpoint = lvl_8.position; }

        if (lvlNumber == 9) { checkpoint = lvl_9.position; }

        if (lvlNumber == 10) { checkpoint = lvl_10.position; }

        if (lvlNumber == 11) { checkpoint = lvl_11.position; }

        if (lvlNumber == 12) { checkpoint = lvl_12.position; }

        if (lvlNumber == 13) { checkpoint = lvl_13.position; }

        if (lvlNumber == 14) { checkpoint = lvl_14.position; }

        if (lvlNumber == 15) { checkpoint = lvl_15.position; }

        if (lvlNumber == 16) { checkpoint = lvl_16.position; }

        if (lvlNumber == 17) { checkpoint = lvl_17.position; }

        if (lvlNumber == 18) { checkpoint = lvl_18.position; }

        if (lvlNumber == 19) { checkpoint = lvl_19.position; }

        if (lvlNumber == 20) { checkpoint = lvl_20.position; }


    }
}
