using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour {
	[Header("References")]
	[SerializeField] private Light2D globalLight;
	[Header("Properties")]
	[SerializeField] private Gradient lightGradient;
	[SerializeField] private float daySeconds;
	[SerializeField] private float nightSeconds;
	[SerializeField] private bool isDay;

	private float timer;

	private void Start ( ) {

	}

	private void Update ( ) {
		// Update the game differently based on if it is daytime or nighttime
		float x = 0;
		if (isDay) {
			// If the timer has reached 0, then switch to nighttime
			if (timer <= 0) {
				isDay = false;
				timer = nightSeconds;
			} else {
				x = (daySeconds - timer) / daySeconds;
			}
		} else {
			// If the timer has reached 0, then switch to daytime
			if (timer <= 0) {
				isDay = true;
				timer = daySeconds;
			}
		}

		// Update the color of the light in the scene
		// -4x^2 + 4x
		globalLight.color = lightGradient.Evaluate(-4 * (Mathf.Pow(x, 2) - x));

		// Decrease the timer based on the time that has passed
		timer -= Time.deltaTime;
	}
}
