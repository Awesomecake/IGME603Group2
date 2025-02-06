using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 0,1,2 for Red, Green, Blue
    [Header("Player ID Variables")]
    [SerializeField] private int playerID;
    public int PlayerID {  get { return playerID; } }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite RedPlayerSprite;
    [SerializeField] private Sprite GreenPlayerSprite;
    [SerializeField] private Sprite BluePlayerSprite;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteAnimations spriteAnimations;

    [Header("Movement Variables")]
    [SerializeField] private float speed;

    [Header("Script References")]
    public PlayerStorage playerStorage;

    private bool isMoving = false;
    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        UpdatePlayerColor();
    }

    private void Update()
    {
        if (isMoving)
            Move();
    }

    public void InputActionMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
        isMoving = moveDirection != Vector2.zero;

        //***** movement animation logic *****
        //if player is not moving,...
        if(moveDirection == Vector2.zero)
        {
            //end walking animation
            spriteAnimations.EndMoving();
        }
        //else if moveDirection is going to the left,...
        else if (moveDirection.x <= 0f)
        {
            //start walking animation to the left
            spriteAnimations.BeginMovingLeft();

            if(playerStorage.carriedResource != null)
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
        switch (playerID%3)
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

#if UNITY_EDITOR
    void OnValidate() { UnityEditor.EditorApplication.delayCall += UpdatePlayerColor; }
#endif
}
