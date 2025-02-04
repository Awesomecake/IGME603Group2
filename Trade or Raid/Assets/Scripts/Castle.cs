using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int playerID;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite redCastleSprite;
    [SerializeField] Sprite greenCastleSprite;
    [SerializeField] Sprite blueCastleSprite;
    [SerializeField] GameObject circleIndicator;

    private void Start()
    {
        UpdateCastleColor();
    }

    void UpdateCastleColor()
    {
        switch (playerID % 3)
        {
            case 1:
                if (greenCastleSprite != null)
                    spriteRenderer.sprite = greenCastleSprite;
                break;
            case 2:
                if (blueCastleSprite != null)
                    spriteRenderer.sprite = blueCastleSprite;
                break;
            default:
                if (redCastleSprite != null)
                    spriteRenderer.sprite = redCastleSprite;
                break;
        }
    }

    public void TurnOnCircleIndicator()
    {
        circleIndicator.SetActive(true);
    }

    public void TurnOffCircleIndicator()
    {
        circleIndicator.SetActive(false);
    }
}
