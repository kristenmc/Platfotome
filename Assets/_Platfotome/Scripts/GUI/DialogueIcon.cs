using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class DialogueIcon : MonoBehaviour {

		private const float FadeTime = 0.2f;

		private Image image;

		private void Awake() {
			image = GetComponentInChildren<Image>();
			StartCoroutine(LerpVisibility(0, 1, false));
		}

		private IEnumerator LerpVisibility(float from, float to, bool destroy) {

			float time = 0f;

			while (time < FadeTime) {
				time += Time.deltaTime;

				image.color = Util.SetAlpha(image.color, Mathf.Lerp(from, to, time / FadeTime));
				yield return new WaitForEndOfFrame();

			}

			image.color = Util.SetAlpha(image.color, to);

			if (destroy) Destroy(gameObject);

		}

		/// <summary>
		/// Trigger the destruction sequence of the icon.
		/// </summary>
		public void TriggerDestroy() {
			StartCoroutine(LerpVisibility(1, 0, true));
		}

		

	}

}