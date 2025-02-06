using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStorage : MonoBehaviour
{
    [SerializeField] Castle castle;
    public int wheatStored = 0;
    [SerializeField] private List<GameObject> wheatStorageVisual;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteAnimations spriteAnimations;

    //Night Cycle
    public void DayPassed()
    {
        //remove wheat
        wheatStored--;
        UpdateWheatVisual();

        if (wheatStored == 0)
        {
            //Player Dies
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null 
            && castle.playerID == playerController.PlayerID 
            && playerController.playerStorage.carriedResource != null)
        {
            StoreWheat(playerController.playerStorage);
        }
    }

    private void StoreWheat(PlayerStorage playerStorage)
    {
        playerStorage.DepositResource();

        spriteAnimations.BeginDepositAnimation();

        if (wheatStored < 10)
        {
            //Add Wheat
            wheatStored++;

            UpdateWheatVisual();

            Debug.Log("Player " + castle.playerID + " Wheat Stored");
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
                    wheatStorageVisual[i].SetActive(true);
                }
                else
                {
                    wheatStorageVisual[i].SetActive(false);
                }
            }
        }
    }
}
