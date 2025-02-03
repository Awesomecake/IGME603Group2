using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStorage : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    public int wheatStored = 0;
    [SerializeField] private List<GameObject> wheatStorageVisual;

    //Night Cycle
    public void DayPassed()
    {
        //remove wheat
        wheatStored--;
        UpdateWheatVisual();

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
            if (wheatStored < 10)
            {
                //Add Wheat
                wheatStored++;

                UpdateWheatVisual();

                Debug.Log("Player " + playerID + " Wheat Stored");
            }
        }
    }

    public void UpdateWheatVisual()
    {
        for (int i = 0; i < wheatStorageVisual.Count; i++)
        {
            if (wheatStorageVisual[i] != null && i < 10)
            {
                if (i < wheatStored)
                {
                    wheatStorageVisual[i].active = true;
                }
                else
                {
                    wheatStorageVisual[i].active = false;
                }
            }
        }
    }
}
