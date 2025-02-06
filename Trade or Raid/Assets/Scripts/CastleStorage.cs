using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStorage : MonoBehaviour
{
    [SerializeField] Castle castle;
    [SerializeField] private int _wheatStored = 0;
    [SerializeField] private List<GameObject> wheatStorageVisual;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteAnimations spriteAnimations;

    /// <summary>
    /// The current number of wheat that is stored within this castle storage
    /// </summary>
    public int WheatStored {
        get => _wheatStored;
        set {
			_wheatStored = value;
            UpdateWheatVisual( );

            // If there are no more wheat remaining, the player has died
            if (_wheatStored == 0) {
                /// TO DO: Make the player able to die
            }
        }
    }

    /// <summary>
    /// Update the castle storage when the day begins
    /// </summary>
    public void OnDayBegin() {
        /// TO DO: Change this to increase based on the days that have passed
        WheatStored--;
    }

    /// <summary>
    /// Update the castle storage when they are raided
    /// </summary>
    public void OnRaid () {
        /// NOTE: Might need to update this value to make raids more worth it
		WheatStored -= 2;
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

    /// <summary>
    /// Store some wheat from a player into this castle storage
    /// </summary>
    /// <param name="playerStorage">The player storage to get the wheat from</param>
    private void StoreWheat(PlayerStorage playerStorage)
    {
        // Update the player's sprite animations
        playerStorage.DepositResource();
        spriteAnimations.BeginDepositAnimation();

        // Add wheat to this castle storage
        WheatStored++;

        Debug.Log("Player " + castle.playerID + " Wheat Stored");
    }

    public void UpdateWheatVisual()
    {
        for (int i = 0; i < wheatStorageVisual.Count; i++)
        {
            if (wheatStorageVisual[i] != null && i < 10)
            {
                if (i < WheatStored)
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
