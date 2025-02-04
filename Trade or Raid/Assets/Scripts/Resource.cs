using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public void GatherResource(Transform player)
    {
        //Add animation here
        transform.parent = player;
        transform.localPosition = new Vector2(0, 1);
        spriteRenderer.sortingOrder = 4;
    }

    public void DepositResource(Transform castle)
    {
        //Add animation here
        gameObject.SetActive(false);
    }
}
