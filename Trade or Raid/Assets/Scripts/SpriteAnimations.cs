using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimations : MonoBehaviour
{
    //General Animation Variables
    private float startingScaleY = 0f;
    private float walkAnimProgress = 0f;
    private float harvestAnimProgress = 0f;
    private float depositAnimProgress = 0f;
    private float jumpAnimProgress = 0f;

    [Header("GameObject Variables")]
    private SpriteRenderer spriteRenderer;

    [Header("Walk Animation Variables")]
    [SerializeField] private float walkWobbleTimeLength = 1f;
    [SerializeField] private float wobbleRotationStrength = 1f;
    [SerializeField] private float walkVerticalStretchStrength = 1f;
    [SerializeField] private float walkHorizontalSquashStrength = 1f;
    [HideInInspector] public bool isMoving = false;

    [Header("Harvest Animation Variables")]
    public float harvestTime = 1f;
    [SerializeField] private float harvestVerticalStretchStrength = 1f;
    [SerializeField] private float harvestHorizontalSquashStrength = 1f;
    [HideInInspector] public bool isBeingHarvested = false;
    [SerializeField] private float fastHarvestAnimMultiplier = 1f;

    [Header("Jump Animation Variables")]
    [SerializeField] private float jumpTimeLength = 1f;
    [SerializeField] private float jumpMaxHeight = 1f;
    [HideInInspector] public bool isJumping = false;
    private Transform targetTransformToJumpTo;
    private Vector3 preJumpPos = Vector3.zero;

    [Header("Deposit Animation Variables")]
    [SerializeField] private float depositWiggleTimeLength = 1f;
    [SerializeField] private float depositVerticalStretchStrength = 1f;
    [SerializeField] private float depositHorizontalSquashStrength = 1f;
    [HideInInspector] public bool isDepositing = false;

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
        //***** Debug inputs REMOVE IF NOT DEBUGGING *****
        #region Debug inputs

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

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    BeginHarvesting();
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    EndHarvesting();
        //}

        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    BeginDepositAnimation();
        //}

        #endregion

        //***** WALK ANIMATION *****
        #region WALK ANIMATION

        if (isMoving)
        {
            //increment walk animation progress
            walkAnimProgress += Time.deltaTime;

            if(walkAnimProgress > 0f && walkAnimProgress <= walkWobbleTimeLength * 0.25f)
            {
                //rotate sprite left
                gameObject.transform.Rotate(0f, 0f, wobbleRotationStrength * Time.deltaTime);

                WalkStretchSpriteUp(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength * 0.25f && walkAnimProgress <= walkWobbleTimeLength * 0.5f)
            {
                //rotate sprite right
                gameObject.transform.Rotate(0f, 0f, -wobbleRotationStrength * Time.deltaTime);

                WalkSquashSpriteDown(Time.deltaTime);
            }
            else if (walkAnimProgress > walkWobbleTimeLength * 0.5f && walkAnimProgress <= walkWobbleTimeLength * 0.75f)
            {
                //rotate sprite right
                gameObject.transform.Rotate(0f, 0f, -wobbleRotationStrength * Time.deltaTime);

                WalkStretchSpriteUp(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength * 0.75f && walkAnimProgress <= walkWobbleTimeLength)
            {
                //rotate sprite left
                gameObject.transform.Rotate(0f, 0f, wobbleRotationStrength * Time.deltaTime);

                WalkSquashSpriteDown(Time.deltaTime);
            }
            else if(walkAnimProgress > walkWobbleTimeLength)
            {
                //reset walkAnimProgress
                walkAnimProgress = 0f;

                Reset_Transform();
            }
        }

        #endregion

        //***** HARVEST ANIMATION *****
        #region HARVEST ANIMATION

        if (isBeingHarvested)
        {
            //increment harvestAnimProgress
            harvestAnimProgress += Time.deltaTime;

            if(harvestAnimProgress > 0f && harvestAnimProgress <= harvestTime * 0.6f)
            {
                //HarvestStretchSpriteUp(Time.deltaTime/harvestTime);
                HarvestStretchSpriteUp(Time.deltaTime);
            }
            else if(harvestAnimProgress > 0.60f && harvestAnimProgress <= harvestTime * 0.65f)
            {
                FastHarvestStretchSpriteDown(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.65f && harvestAnimProgress <= harvestTime * 0.70f)
            {
                FastHarvestStretchSpriteUp(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.70f && harvestAnimProgress <= harvestTime * 0.75f)
            {
                FastHarvestStretchSpriteDown(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.75f && harvestAnimProgress <= harvestTime * 0.80f)
            {
                FastHarvestStretchSpriteUp(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.80f && harvestAnimProgress <= harvestTime * 0.85f)
            {
                FastHarvestStretchSpriteDown(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.85f && harvestAnimProgress <= harvestTime * 0.90f)
            {
                FastHarvestStretchSpriteUp(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.90f && harvestAnimProgress <= harvestTime * 0.95f)
            {
                FastHarvestStretchSpriteDown(Time.deltaTime);
            }
            else if (harvestAnimProgress > 0.95f && harvestAnimProgress <= harvestTime)
            {
                FastHarvestStretchSpriteUp(Time.deltaTime);
            }
            else if(harvestAnimProgress > harvestTime)
            {
                EndHarvesting();
            }
        }

        #endregion

        //***** JUMP TO POINT ANIMATION *****
        #region JUMP TO POINT ANIMATION

        if(isJumping)
        {
            //increment jumpAnimProgress
            jumpAnimProgress += Time.deltaTime;

            if(jumpAnimProgress > 0f && jumpAnimProgress <= jumpTimeLength)
            {
                JumpUp(Time.deltaTime);
            }
            else if(jumpAnimProgress > jumpTimeLength)
            {
                EndJumpAnimation();
            }
        }

        #endregion

        //***** DEPOSIT ANIMATION *****
        #region DEPOSIT ANIMATION

        if (isDepositing)
        {
            //increment depositAnimProgress
            depositAnimProgress += Time.deltaTime;

            if(depositAnimProgress > 0f && depositAnimProgress <= depositWiggleTimeLength * 0.5f)
            {
                DepositSquashSpriteDown(Time.deltaTime);
            }
            else if(depositAnimProgress > depositWiggleTimeLength * 0.5f && depositAnimProgress <= depositWiggleTimeLength)
            {
                DepositStretchSpriteUp(Time.deltaTime);
            }
            else if (depositAnimProgress > depositWiggleTimeLength)
            {
                EndDepositAnimation();
            }
        }

        #endregion
    }

    //***** Walk Animation Functions *****
    #region Walk Animation Functions

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

    private void WalkStretchSpriteUp(float deltaTime)
    {
        //stretch sprite vertically and shrink sprite horizontally
        gameObject.transform.localScale += new Vector3(-walkHorizontalSquashStrength, walkVerticalStretchStrength, 0) * deltaTime;
    }

    private void WalkSquashSpriteDown(float deltaTime)
    {
        //shrink sprite vertically and stretch sprite horizontally
        gameObject.transform.localScale += new Vector3(walkHorizontalSquashStrength, -walkVerticalStretchStrength, 0) * deltaTime;
    }

    #endregion

    //***** Harvest Animation Functions *****
    #region Harvest Animation Functions

    public void BeginHarvesting()
    {
        //set isBeingHarvested to true
        isBeingHarvested = true;
    }

    public void BeginHarvesting(float harvestTime)
    {
        this.harvestTime = harvestTime;
        //set isBeingHarvested to true
        isBeingHarvested = true;
    }

    public void EndHarvesting()
    {
        //set isBeingHarvested to false
        isBeingHarvested = false;

        //reset harvestAnimProgress
        harvestAnimProgress = 0f;

        //reset transform
        Reset_Transform();

        //BeginJumpAnimation(GameObject.Find("TempGameObject").transform);
    }

    private void HarvestStretchSpriteUp(float deltaTime)
    {
        //stretch sprite vertically and shrink sprite horizontally
        gameObject.transform.localScale += new Vector3(-harvestHorizontalSquashStrength, harvestVerticalStretchStrength, 0) * deltaTime;
        //gameObject.transform.localScale = Vector3.Lerp(-harvestHorizontalSquashStrength, harvestVerticalStretchStrength, 0) * deltaTime;
    }

    private void FastHarvestStretchSpriteUp(float deltaTime)
    {
        //stretch sprite vertically and shrink sprite horizontally
        gameObject.transform.localScale += new Vector3(-harvestHorizontalSquashStrength * fastHarvestAnimMultiplier, harvestVerticalStretchStrength * fastHarvestAnimMultiplier, 0) * deltaTime;
    }

    private void FastHarvestStretchSpriteDown(float deltaTime)
    {
        //shrink sprite vertically and stretch sprite horizontally
        gameObject.transform.localScale += new Vector3(harvestHorizontalSquashStrength * fastHarvestAnimMultiplier, -harvestVerticalStretchStrength * fastHarvestAnimMultiplier, 0) * deltaTime;
    }

    #endregion

    //***** Jump Animation Functions *****
    #region Jump Animation Functions

    public void BeginJumpAnimation(Transform transformToJumpTo)
    {
        //Debug.Log("Start");
        //set isJumping to true
        isJumping = true;

        //set preJumpPos
        preJumpPos = gameObject.transform.position;

        //set targetTransformToJumpTo to transformToJumpTo
        targetTransformToJumpTo = transformToJumpTo;
    }

    public void EndJumpAnimation()
    {
        //Debug.Log("End");
        //set isJumping to false
        isJumping = false;

        //reset jumpAnimProgress to 0f
        jumpAnimProgress = 0f;

        //attach resource to the player that harvested it 
        //gameObject.transform.parent.gameObject.GetComponent<Resource>().AttachToPlayer(targetTransformToJumpTo);
    }

    private void JumpUp(float deltaTime)
    {
        //Debug.Log("Jumping!");
        //change y value

        //change x value as long as targetTransformToJumpTo exists
        if (targetTransformToJumpTo)
            Vector3.MoveTowards(preJumpPos, targetTransformToJumpTo.position, deltaTime / jumpTimeLength);
    }

    #endregion

    //***** Depositing Animation Functions *****
    #region Depositing Animation Functions

    public void BeginDepositAnimation()
    {
        //set isDepositing to true
        isDepositing = true;

        //reset depositAnimProgress to zero
        depositAnimProgress = 0f;
    }

    public void EndDepositAnimation()
    {
        //set isDepositing to false
        isDepositing = false;

        //reset depositAnimProgress to zero
        depositAnimProgress = 0f;

        //reset transform
        Reset_Transform();
    }

    private void DepositStretchSpriteUp(float deltaTime)
    {
        //stretch sprite vertically and shrink sprite horizontally
        gameObject.transform.localScale += new Vector3(-depositHorizontalSquashStrength, depositVerticalStretchStrength, 0) * deltaTime;
    }

    private void DepositSquashSpriteDown(float deltaTime)
    {
        //shrink sprite vertically and stretch sprite horizontally
        gameObject.transform.localScale += new Vector3(depositHorizontalSquashStrength, -depositVerticalStretchStrength, 0) * deltaTime;
    }

    #endregion

    //***** Helper Functions *****
    private void Reset_Transform()
    {
        //reset all parts of the transform
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }
}
