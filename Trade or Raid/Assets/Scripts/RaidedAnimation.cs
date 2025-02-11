using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidedAnimation : MonoBehaviour
{
    [SerializeField] float growTime, displayTime, shrinkTime;

    private void OnEnable()
    {
        StartCoroutine(DisplayAnimationCoroutine());
    }

    IEnumerator DisplayAnimationCoroutine()
    {
        transform.localScale = Vector3.zero;
        float elapsedTime = 0;
        while (elapsedTime < growTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.one * elapsedTime / growTime;
        }

        elapsedTime = 0;
        while (elapsedTime < displayTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        elapsedTime = 0;
        while (elapsedTime < shrinkTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.one - (Vector3.one * (elapsedTime / shrinkTime));
        }

        gameObject.SetActive(false);
    }
}
