using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;

    //Pause Delay for multiple inputs
    private bool canPause = true;

    public void TogglePause()
    {
        Debug.Log(canPause);

        if (!canPause)
            return;

        isPaused = !isPaused;

        if(isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }

        canPause = false;
        StartCoroutine(PauseDelay());
    }

    private IEnumerator PauseDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        canPause = true;
    }

    public void InputActionPause(InputAction.CallbackContext context)
    {
        TogglePause();
    }
}
