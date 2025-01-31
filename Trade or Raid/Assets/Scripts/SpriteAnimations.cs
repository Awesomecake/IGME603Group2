using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("GameObject Variables")]
    [SerializeField] private GameObject gameObjectToAnimate;

    [Header("Walk Animation Variables")]
    [SerializeField] private float wobbleSpeed = 0f;
    [SerializeField] private float wobbleHorizontalStrength = 0f;
    [SerializeField] private float wobbleVerticalStrength = 0f;
    private bool isMoving = false;
    private float startingScaleY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //get sprite renderer of gameObjectToAnimate
        spriteRenderer = gameObjectToAnimate.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogWarning("Please add a SpriteRenderer Component to " + gameObjectToAnimate.name);

        //get startingScaleY
        startingScaleY = gameObjectToAnimate.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug inputs REMOVE IF NOT DEBUGGING
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BeginMovingLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            BeginMovingRight();
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            EndMoving();
        }

        //***** WALK ANIMATION *****
        if(isMoving)
        {
            gameObjectToAnimate.transform.localScale += new Vector3(0f, Mathf.Lerp(0f, startingScaleY * wobbleVerticalStrength,  Time.deltaTime * wobbleSpeed), 0f);
        }
    }

    public void BeginMovingLeft()
    {
        //flip sprite to left
        spriteRenderer.flipX = false;

        //
        BeginMoving();
    }

    public void BeginMovingRight()
    {
        //flip sprite to right
        spriteRenderer.flipX = true;

        //
        BeginMoving();
    }

    private void BeginMoving()
    {
        //set isMoving to true
        isMoving = true;
    }

    public void EndMoving()
    {
        //set isMoving to false
        isMoving = false;
    }

    
}
