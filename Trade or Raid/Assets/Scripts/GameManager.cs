using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour {
	[Header("References")]
	[SerializeField] private Light2D globalLight;
	[Space]
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private TextMeshProUGUI dayText;
	[SerializeField] private TextMeshProUGUI objectiveText;
	[Space]
	[SerializeField] private CastleStorage[ ] castleStorages;
	[SerializeField] private WheatSpawner wheatSpawner;
	[Header("Properties")]
	[SerializeField] private Gradient lightGradient;
	[SerializeField] private float daySeconds;
	[SerializeField] private float nightSeconds;
	[SerializeField] private bool isDay;
	[SerializeField] private int dayCount;

	private float timer;

	public static GameManager instance;

	public UnityEvent OnDaybreak = new UnityEvent();
	public UnityEvent OnNightfall = new UnityEvent();


	private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update ( ) {
		// Update the game differently based on if it is daytime or nighttime
		float x = 0;
		if (isDay) {
			// If the timer has reached 0, then switch to nighttime
			if (timer <= 0) {
				isDay = false;
				timer = nightSeconds;

				OnNightfall?.Invoke();

				//Despawn all wheat
				wheatSpawner.DespawnAllWheat();
			} else {
				x = (daySeconds - timer) / daySeconds;
			}
		} else {
			// If the timer has reached 0, then switch to daytime
			if (timer <= 0) {
				isDay = true;
				timer = daySeconds;
                OnDaybreak?.Invoke();

                // Increment the day number
                dayCount++;
				dayText.text = $"Day {dayCount}";

				// Decrease the wheat stored in the castle storages
				foreach (CastleStorage castleStorage in castleStorages) {
					castleStorage.OnDayBegin( );
				}

				//Spawn wheat for new day
				wheatSpawner.NewDay();
			}
		}

		// Update the color of the light in the scene
		// -4x^2 + 4x
		globalLight.color = lightGradient.Evaluate(-4 * (Mathf.Pow(x, 2) - x));

		// Decrease the timer based on the time that has passed
		string dayNightText = (isDay ? "Night" : "Day");
		timerText.text = $"Time Until {dayNightText}: {Mathf.CeilToInt(timer)}s";
		timer -= Time.deltaTime;
	}

	public void RaidPlayer1 () {
		// If it is currently daytime, there is no raiding
		if (isDay) {
			return;
		}

		castleStorages[0].OnRaid( );
	}

	public void RaidPlayer2 () {
		// If it is currently daytime, there is no raiding
		if (isDay) {
			return;
		}

		castleStorages[1].OnRaid( );
	}

	public void RaidPlayer3 () {
		// If it is currently daytime, there is no raiding
		if (isDay) {
			return;
		}

		castleStorages[2].OnRaid( );
	}

	public void DonateToPlayer(int playerIndex)
	{
		castleStorages[playerIndex].OnDonation();
	}
}
