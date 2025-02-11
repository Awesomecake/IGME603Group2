using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        }
    }

    public void RaidPlayer2(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 1)
        {
            Debug.Log("Player " + playerID + " trying to Raid Player 2");

            hasPlayerRaided = true;
            GameManager.instance.RaidPlayer2();
        }
    }

    public void RaidPlayer3(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 2)
        {
            Debug.Log("Player " + playerID + " trying to Raid Player 3");

            hasPlayerRaided = true;
            GameManager.instance.RaidPlayer3();
        }
    }

    public void DonateToPlayer1(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 0)
        {
            Debug.Log("Player " + playerID + " trying to donate to Player 1");

            hasPlayerRaided = true;
            GameManager.instance.DonateToPlayer(0);
        }
    }

    public void DonateToPlayer2(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 1)
        {
            Debug.Log("Player " + playerID + " trying to donate to Player 2");

            hasPlayerRaided = true;
            GameManager.instance.DonateToPlayer(1);
        }
    }

    public void DonateToPlayer3(InputAction.CallbackContext context)
    {
        if (!hasPlayerRaided && playerID != 2)
        {
            Debug.Log("Player " + playerID + " trying to donate to Player 3");

            hasPlayerRaided = true;
            GameManager.instance.DonateToPlayer(2);
        }
    }

#if UNITY_EDITOR
    void OnValidate() { UpdatePlayerColor(); }
#endif
}
