using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class CastleStorage : MonoBehaviour
{
    [SerializeField] Castle castle;
    [SerializeField] private int _wheatStored = 0;
    [SerializeField] private TextMeshProUGUI wheatStoreText;
    [SerializeField] bool hasStorageBeenRaided = false;
    [SerializeField] GameObject raidedMessage;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteAnimations spriteAnimations;

    /// <summary>
    /// The current number of wheat that is stored within this castle storage
    /// </summary>
    public int WheatStored
    {
        get => _wheatStored;
        set
        {
            _wheatStored = value;
            UpdateWheatVisual();

            // If there are no more wheat remaining, the player has died
            if (_wheatStored < 0)
            {
                // Sorry this code kind of sucks, I didn't have time to come up with a better solution - Frankie
				GameManager.instance.GetComponent<DataTracker>( ).LogAction($"Player {castle.playerID + 1} has starved to death.");
				GameManager.instance.GetComponent<DataTracker>( ).LogConclusion( );
				SceneManager.LoadScene("MainMenuScene");
			}
        }
    }

    /// <summary>
    /// Update the castle storage when the day begins
    /// </summary>
    public void OnDayBegin()
    {
        /// TO DO: Change this to increase based on the days that have passed
        WheatStored--;

        if (hasStorageBeenRaided)
        {
            Debug.Log("You've been raided!");
            raidedMessage.SetActive(true);
            hasStorageBeenRaided = false;
        }
    }

    /// <summary>
    /// Update the castle storage when they are raided
    /// </summary>
    public void OnRaid()
    {
        /// NOTE: Might need to update this value to make raids more worth it
        _wheatStored -= 2;
        hasStorageBeenRaided = true;
        //WheatStored -= 2;
    }

	//public void OnDonation()
	//{
	//    _wheatStored += 3;
	//}

	//public void OnTriggerEnter2D(Collider2D collision)
	//{
	//    PlayerController playerController = collision.GetComponent<PlayerController>();
	//    if (playerController != null
	//        && castle.playerID == playerController.PlayerID
	//        && playerController.playerStorage.carriedResource != null)
	//    {
	//        StoreWheat(playerController.playerStorage);
	//    }
	//}

	private void Start ( ) {
        UpdateWheatVisual( );
	}

	public void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null
            //&& castle.playerID == playerController.PlayerID
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

        //if player deposited wheat into a castle that is NOT their own,...
        if (playerStorage.gameObject.GetComponent<PlayerController>().PlayerID != castle.playerID)
        {
            //add the appropriate donation log
            switch(castle.playerID)
            {
                case 0:
                    playerStorage.gameObject.GetComponent<PlayerController>().DonateToPlayer1();
                    break;
                case 1:
                    playerStorage.gameObject.GetComponent<PlayerController>().DonateToPlayer2();
                    break;
                case 2:
                    playerStorage.gameObject.GetComponent<PlayerController>().DonateToPlayer3();
                    break;
            }
        }

        //Debug.Log("Player " + (playerStorage.gameObject.GetComponent<PlayerController>().PlayerID + 1) + " stored wheat into Player " + (castle.playerID + 1) + "'s Castle");
    }

    public void UpdateWheatVisual()
    {
        wheatStoreText.text = "x" + WheatStored.ToString();
    }
}
