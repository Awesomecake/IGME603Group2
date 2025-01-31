using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 0,1,2 for Red, Green, Blue
    [SerializeField] private int playerID;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite RedPlayerSprite;
    [SerializeField] private Sprite GreenPlayerSprite;
    [SerializeField] private Sprite BluePlayerSprite;

    [SerializeField] private float speed = 0.01f;

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
    }

    void Move()
    {
        transform.Translate(moveDirection * speed);
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
