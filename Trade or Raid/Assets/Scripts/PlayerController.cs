using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    static string fileName = "DataTracking.txt";

    // 0,1,2 for Red, Green, Blue
    [Header("Player ID Variables")]
    [SerializeField] private int playerID;
    public int PlayerID { get { return playerID; } }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite RedPlayerSprite;
    [SerializeField] private Sprite GreenPlayerSprite;
    [SerializeField] private Sprite BluePlayerSprite;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteAnimations spriteAnimations;

    [Header("Movement Variables")]
    [SerializeField] private float speed;

    [Header("State variables")]
    [SerializeField] bool hasPlayerRaided = false;

    [Header("Script References")]
    public PlayerStorage playerStorage;
    [SerializeField] private GameManager gameManager;

    private bool isMoving = false;
    private Vector2 moveDirection = Vector2.zero;

    //Data Tracking Variables
    int player1raids = 0;
    int player2raids = 0;
    int player3raids = 0;
    int player1donations = 0;
    int player2donations = 0;
    int player3donations = 0;

    private void Start()
    {
        UpdatePlayerColor();
    }

    private void OnEnable()
    {
        GameManager.instance.OnNightfall.AddListener(() => hasPlayerRaided = false);
        GameManager.instance.OnDaybreak.AddListener(() => hasPlayerRaided = true);
    }

    private void OnDisable()
    {
        GameManager.instance.OnNightfall.RemoveAllListeners();
        GameManager.instance.OnDaybreak.RemoveAllListeners();
    }

    private void Update()
    {
        if (isMoving)
            Move();
    }

    public void InputActionMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
        if (!gameManager.isDay)
            moveDirection = Vector2.zero;

        isMoving = (moveDirection != Vector2.zero && gameManager.isDay);

        //***** movement animation logic *****
        //if player is not moving,...
        if (moveDirection == Vector2.zero)
        {
            //end walking animation
            spriteAnimations.EndMoving();
        }
        //else if moveDirection is going to the left,...
        else if (moveDirection.x <= 0f)
        {
            //start walking animation to the left
            spriteAnimations.BeginMovingLeft();

            if (playerStorage.carriedResource != null)
            {
                playerStorage.carriedResource.spriteAnimations.BeginMovingLeft();
            }
        }
        //else moveDirection is going to the right,...
        else
        {
            //start walking animation to the right
            spriteAnimations.BeginMovingRight();

            if (playerStorage.carriedResource != null)
            {
                playerStorage.carriedResource.spriteAnimations.BeginMovingLeft();
            }
        }
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void UpdatePlayerColor()
    {
        switch (playerID % 3)
        {
            case 1:
                if (GreenPlayerSprite != null)
                    spriteRenderer.sprite = GreenPlayerSprite;
                break;
            case 2:
                if (BluePlayerSprite != null)
                    spriteRenderer.sprite = BluePlayerSprite;
                break;
            default:
                if (RedPlayerSprite != null)
                    spriteRenderer.sprite = RedPlayerSprite;
                break;
        }
    }

    public void RaidPlayer1(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 0)
        {
            Debug.Log("Player " + playerID + " trying to Raid Player 1");

            hasPlayerRaided = true;
            GameManager.instance.RaidPlayer1();

            player1raids++;
            CreateLog($" > Player {playerID} raided Player 1");
        }
    }

    public void RaidPlayer2(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 1)
        {
            Debug.Log("Player " + playerID + " trying to Raid Player 2");

            hasPlayerRaided = true;
            GameManager.instance.RaidPlayer2();

            player2raids++;
            CreateLog($" > Player {playerID} raided Player 2");
        }
    }

    public void RaidPlayer3(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 2)
        {
            Debug.Log("Player " + playerID + " trying to Raid Player 3");

            hasPlayerRaided = true;
            GameManager.instance.RaidPlayer3();

            player3raids++;
            CreateLog($" > Player {playerID} raided Player 3");
        }
    }

    public void DonateToPlayer1()
    {
        //if (!hasPlayerRaided && playerID != 0)
        //{
        Debug.Log("Player " + playerID + " trying to donate to Player 1");

        //    hasPlayerRaided = true;
        //    GameManager.instance.DonateToPlayer(0);

        player1donations++;
        CreateLog($" > Player {playerID} donated to Player 1");
        //}
    }

    public void DonateToPlayer2()
    {
        //if (!hasPlayerRaided && playerID != 1)
        //{
              Debug.Log("Player " + playerID + " trying to donate to Player 2");

        //    hasPlayerRaided = true;
        //    GameManager.instance.DonateToPlayer(1);

        player2donations++;
        CreateLog($" > Player {playerID} donated to Player 2");
        //}
    }

    //InputAction.CallbackContext context
    public void DonateToPlayer3()
    {
        //if (!hasPlayerRaided && playerID != 2)
        //{
            Debug.Log("Player " + playerID + " trying to donate to Player 3");

            //hasPlayerRaided = true;
            //GameManager.instance.DonateToPlayer(2);

            player3donations++;
            CreateLog($" > Player {playerID} donated to Player 3");
        //}
    }

    public void OnDestroy()
    {
        CreateLog($"Final Stats for Player {playerID}:");
        CreateLog($" > Raided Player 1 {player1raids} times, Donated {player1donations} times");
        CreateLog($" > Raided Player 2 {player2raids} times, Donated {player2donations} times");
        CreateLog($" > Raided Player 3 {player3raids} times, Donated {player3donations} times");
    }

    void CreateLog(string output)
    {
        // This text is added only once to the file.
        if (!File.Exists(fileName))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("First Action : " + output);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(output);
            }
        }
    }

#if UNITY_EDITOR
    void OnValidate() { UpdatePlayerColor(); }
#endif
}
