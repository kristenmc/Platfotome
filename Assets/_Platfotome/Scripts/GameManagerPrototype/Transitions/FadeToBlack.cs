using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class FadeToBlack : Transition {

		private Image image;

		private void Awake() {
			image = GetComponent<Image>();
		}

		private void Start() {
			StartCoroutine(Coroutine());
		}

		private IEnumerator Coroutine() {

			float startTime = Time.unscaledTime;

			while (Time.unscaledTime - startTime < arguments.InTime) {
				SetColor((Time.unscaledTime - startTime) / arguments.InTime);
				yield return null;
			}

			SetColor(1);

			startTime = Time.unscaledTime;
			yield return WaitUntilSceneLoad;
			float elapsed = Time.unscaledTime - startTime;
			if (elapsed < arguments.HoldTime) {
				yield return new WaitForSecondsRealtime(arguments.HoldTime - elapsed);
			}

			startTime = Time.unscaledTime;
			while (Time.unscaledTime - startTime < arguments.OutTime) {
				SetColor(1 - (Time.unscaledTime - startTime) / arguments.OutTime);
				yield return null;
			}

			SetColor(0);

			yield return null;
			SetTransitionComplete();
			Destroy(gameObject);
		}

		private void SetColor(float alpha) {
			Color c = image.color;
			c.a = Mathf.Clamp01(alpha);
			image.color = c;
		}

	}

}