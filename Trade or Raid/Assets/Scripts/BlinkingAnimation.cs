using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkingAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkSpeed;
    [SerializeField] float minAlpha;
    [SerializeField] float maxAlpha;

    Coroutine animationCoroutine;

    private void OnEnable()
    {
        animationCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator BlinkCoroutine()
    {
        bool reduceAlpha = true;
        while (true)
        {
            if (reduceAlpha)
            {
                spriteRenderer.color = new Color(
                    spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                    spriteRenderer.color.a - blinkSpeed * Time.deltaTime);

                if (spriteRenderer.color.a <= minAlpha)
                {
                    reduceAlpha = false;
                }

            }
            else
            {
                spriteRenderer.color = new Color(
                    spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                    spriteRenderer.color.a + blinkSpeed * Time.deltaTime);

                if (spriteRenderer.color.a >= maxAlpha)
                {
                    reduceAlpha = true;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
