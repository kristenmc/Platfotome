using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class IrisTransition : Transition {

		public Image iris;

		private void Start() {
			StartCoroutine(Coroutine());
		}

		private IEnumerator Coroutine() {

			float startTime = Time.unscaledTime;

			while (Time.unscaledTime - startTime < arguments.InTime) {
				SetParametric((Time.unscaledTime - startTime) / arguments.InTime);
				yield return null;
			}

			SetParametric(1);

			startTime = Time.unscaledTime;
			yield return WaitUntilSceneLoad;
			float elapsed = Time.unscaledTime - startTime;
			if (elapsed < arguments.HoldTime) {
				yield return new WaitForSecondsRealtime(arguments.HoldTime - elapsed);
			}

			startTime = Time.unscaledTime;
			while (Time.unscaledTime - startTime < arguments.OutTime) {
				SetParametric(1 - (Time.unscaledTime - startTime) / arguments.OutTime);
				yield return null;
			}

			SetParametric(0);

			yield return null;
			SetTransitionComplete();
			Destroy(gameObject);
		}

		private void SetParametric(float t) {
			iris.pixelsPerUnitMultiplier = Mathf.Lerp(0, 0.1f, t);
		}

	}

}