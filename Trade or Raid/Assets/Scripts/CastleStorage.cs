using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStorage : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    public int wheatStored = 0;

    //Night Cycle
    public void DayPassed()
    {
        //remove wheat
        wheatStored--;

        if (wheatStored == 0 )
        {
            //Player Dies
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null && playerID == playerController.PlayerID)
        {
            //Add Wheat
            wheatStored++;

            Debug.Log("Player " + playerID + " Wheat Stored");
        }
    }
}
