using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimations : MonoBehaviour
{
    [Header("GameObject Variables")]
    private SpriteRenderer spriteRenderer;

    [Header("Walk Animation Variables")]
    [SerializeField] private float walkWobbleTimeLength = 1f;
    [SerializeField] private float wobbleRotationStrength = 1f;
    [SerializeField] private float walkVerticalStretchStrength = 1f;
    [SerializeField] private float walkHorizontalSquashStrength = 1f;
    [HideInInspector] public bool isMoving = false;
    private float startingScaleY = 0f;
    private float walkAnimProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //get spriteRenderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //get startingScaleY
        startingScaleY = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug inputs REMOVE IF NOT DEBUGGING
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    BeginMovingLeft();
        //}
        //else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    EndMoving();
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    BeginMovingRight();
        //}
        //else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    EndMoving();
        //}

        //***** WALK ANIMATION *****
        if (isMoving)
        {
            //increment walk animation progress
            walkAnimProgress += Time.deltaTime;

            if(walkAnimProgress > 0f && walkAnimProgress <= walkWobbleTimeLength * 0.25f)
            {
                //rotate sprite left
                gameObject.transform.Rotate(0f, 0f, wobbleRotationStrength * Time.deltaTime);

                StretchSpriteUp(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength * 0.25f && walkAnimProgress <= walkWobbleTimeLength * 0.5f)
            {
                //rotate sprite right
                gameObject.transform.Rotate(0f, 0f, -wobbleRotationStrength * Time.deltaTime);

                SquashSpriteDown(Time.deltaTime);
            }
            else if (walkAnimProgress > walkWobbleTimeLength * 0.5f && walkAnimProgress <= walkWobbleTimeLength * 0.75f)
            {
                //rotate sprite right
                gameObject.transform.Rotate(0f, 0f, -wobbleRotationStrength * Time.deltaTime);

                StretchSpriteUp(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength * 0.75f && walkAnimProgress <= walkWobbleTimeLength)
            {
                //rotate sprite left
                gameObject.transform.Rotate(0f, 0f, wobbleRotationStrength * Time.deltaTime);

                SquashSpriteDown(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength)
            {
                //reset walkAnimProgress
                walkAnimProgress = 0f;

                Reset_Transform();
            }
        }
    }

    public void BeginMovingLeft()
    {
        //flip sprite to left
        spriteRenderer.flipX = false;

        //Begin moving
        BeginMoving();
    }

    public void BeginMovingRight()
    {
        //flip sprite to right
        spriteRenderer.flipX = true;

        //Begin moving
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
        //reset all parts of the transform
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }
    private void StretchSpriteUp(float deltaTime)
    {
        //stretch sprite vertically and shrink sprite horizontally
        gameObject.transform.localScale += new Vector3(-walkHorizontalSquashStrength, walkVerticalStretchStrength, 0) * deltaTime;
    }

    private void SquashSpriteDown(float deltaTime)
    {
        //shrink sprite vertically and stretch sprite horizontally
        gameObject.transform.localScale += new Vector3(walkHorizontalSquashStrength, -walkVerticalStretchStrength, 0) * deltaTime;
    }
}
