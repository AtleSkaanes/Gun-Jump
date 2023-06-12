using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gameMasterData
{
    public int deaths;
    public float timer;

    public int highDeaths;
    public float highTimer;

    public int googogaga;

    public gameMasterData(GameMaster gameMaster)
    {
        /*
        if (gameMaster.GetComponent<GameMaster>().newDeathHigh)
        {
            highDeaths = gameMaster.GetComponent<GameMaster>().deaths;
        }
        else { highDeaths = highDeaths; }
        
        if (gameMaster.GetComponent<GameMaster>().newTimerHigh)
        {
            highTimer = gameMaster.GetComponent<GameMaster>().timer;
        }
        else { highTimer = highTimer; }

        highDeaths = highDeaths;
        highTimer = highTimer;
        */

    }

}
