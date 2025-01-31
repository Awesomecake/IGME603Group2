using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    [Header("GameObject Variables")]
    [SerializeField] private GameObject animationPivotObject;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Walk Animation Variables")]
    [SerializeField] private float walkWobbleTimeLength = 0f;
    [SerializeField] private float wobbleHorizontalStrength = 0f;
    [SerializeField] private float wobbleVerticalStrength = 0f;
    private bool isMoving = false;
    private float startingScaleY = 0f;
    private float walkAnimProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //check sprite renderer of animationPivotObject
        //if (spriteRenderer == null)
        //    Debug.LogWarning("Please add a SpriteRenderer Component to " + animationPivotObject.name);

        //get startingScaleY
        startingScaleY = animationPivotObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug inputs REMOVE IF NOT DEBUGGING
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BeginMovingLeft();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            EndMoving();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
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
            //increment walk animation progress
            walkAnimProgress += Time.deltaTime;

            if(walkAnimProgress > 0f && walkAnimProgress <= walkWobbleTimeLength * 0.5f)
            {
                //rotate sprite left
                animationPivotObject.transform.Rotate(0f, 0f, wobbleHorizontalStrength * Time.deltaTime);

                //stretch sprite vertically and shrink sprite horizontally
                //animationPivotObject.transform.localScale += new Vector3(-1, 1, 0) * Time.deltaTime;
            }
            else if(walkAnimProgress > walkWobbleTimeLength * 0.5f && walkAnimProgress <= walkWobbleTimeLength)
            {
                //rotate sprite right
                animationPivotObject.transform.Rotate(0f, 0f, -wobbleHorizontalStrength * Time.deltaTime);

                //shrink sprite vertically and stretch sprite horizontally
                //animationPivotObject.transform.localScale += new Vector3(1, -1, 0) * Time.deltaTime;
            }
            else if(walkAnimProgress > walkWobbleTimeLength)
            {
                //reset walkAnimProgress
                walkAnimProgress = 0f;
            }
            //animationPivotObject.transform.localScale += new Vector3(0f, Mathf.Lerp(0f, startingScaleY * wobbleVerticalStrength,  Time.deltaTime * wobbleSpeed), 0f);
            //animationPivotObject.transform.localScale += new Vector3(0f, Mathf.PingPong(Time.time * wobbleSpeed, startingScaleY * wobbleVerticalStrength), 0f);
            //animationPivotObject.transform.localScale += ;
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

        //reset walkAnimProgress
        walkAnimProgress = 0f;

        //reset transform
        Reset_Transform();
    }
    private void Reset_Transform()
    {
        animationPivotObject.transform.localPosition = Vector3.zero;
        animationPivotObject.transform.localRotation = Quaternion.identity;
        animationPivotObject.transform.localScale = Vector3.one;
    }
}
