using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightControlsUI : MonoBehaviour
{
    [SerializeField] GameObject nightControlsPanel;
    private void OnEnable()
    {
        GameManager.instance.OnNightfall.AddListener(() =>
        {
            Debug.Log("Night is upon us");
            nightControlsPanel.SetActive(true);
        }
        );
    GameManager.instance.OnDaybreak.AddListener(() => nightControlsPanel.SetActive(false));
    }
}
