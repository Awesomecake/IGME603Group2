using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 0.01f;
    
    private bool isMoving = false;
    private Vector2 moveDirection = Vector2.zero;

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
}
