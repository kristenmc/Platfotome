using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class SpiralTransition : Transition {

		public Transform left;
		public Transform right;

		private void Awake() {
			left.localRotation = Quaternion.identity;
			right.localRotation = Quaternion.identity;
		}

		private void Start() {
			StartCoroutine(Coroutine());
		}

		private IEnumerator Coroutine() {

			float startTime = Time.unscaledTime;

			while (Time.unscaledTime - startTime < arguments.InTime) {
				SetValue((Time.unscaledTime - startTime) / arguments.InTime);
				yield return null;
			}

			SetValue(1);

			startTime = Time.unscaledTime;
			yield return WaitUntilSceneLoad;
			float elapsed = Time.unscaledTime - startTime;
			if (elapsed < arguments.HoldTime) {
				yield return new WaitForSecondsRealtime(arguments.HoldTime - elapsed);
			}

			startTime = Time.unscaledTime;
			while (Time.unscaledTime - startTime < arguments.OutTime) {
				SetValue(1 + (Time.unscaledTime - startTime) / arguments.OutTime);
				yield return null;
			}

			SetValue(2);

			yield return null;
			SetTransitionComplete();
			Destroy(gameObject);
		}

		private void SetValue(float t) {
			left.localRotation = Quaternion.Euler(0, 0, t * 180);
			right.localRotation = Quaternion.Euler(0, 0, t * 180);
		}

	}

}