using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D collider;
    [SerializeField] public SpriteAnimations spriteAnimations;
    [SerializeField] float harvestTime = 1;
    [SerializeField] Image circleTimer;

    Coroutine GatherCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null && playerController.playerStorage.carriedResource == null)
        {
            GatherCoroutine = StartCoroutine(HarvestTimerCoroutine(playerController.playerStorage));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null && playerController.playerStorage.carriedResource == null)
        {
            StopCoroutine(GatherCoroutine);
            spriteAnimations.EndHarvesting();
            circleTimer.fillAmount = 0;
        }
    }

    public void AttachToPlayer(Transform player)
    {
        spriteAnimations.BeginHarvesting();
        transform.parent = player;
        transform.localPosition = new Vector2(0.3f, 0);
        transform.eulerAngles = new Vector3(0, 0, -15);
        collider.enabled = false;
        spriteRenderer.sortingOrder = 4;
    }

    public void DepositResource(Transform castle)
    {
        //Add animation here
        gameObject.SetActive(false);
    }

    IEnumerator HarvestTimerCoroutine(PlayerStorage playerStorage)
    {
        spriteAnimations.BeginHarvesting(harvestTime);
        float elapsedTime = 0;
        while (elapsedTime < harvestTime)
        {
            yield return new WaitForEndOfFrame();

            circleTimer.fillAmount = 1 - elapsedTime / harvestTime;

            if (playerStorage.carriedResource != null)
            {
                break;
            }
            elapsedTime += Time.deltaTime;
        }

        if (playerStorage.carriedResource == null)
        {
            playerStorage.PickUpResource(this);
            AttachToPlayer(playerStorage.transform);
        }
        else
        {
            spriteAnimations.EndHarvesting();
            circleTimer.fillAmount = 0;
        }
    }
}
