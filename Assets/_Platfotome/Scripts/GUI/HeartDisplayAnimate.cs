using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class HeartDisplayAnimate : MonoBehaviour {

		private Vector2 origPos;
		private Image image;

		private void Start() {
			origPos = transform.position;
			image = GetComponentInChildren<Image>();
		}

		private IEnumerator Gain() {
			Vector2 offset = origPos + new Vector2(0, 3f);

			SimpleTimer timer = new SimpleTimer(0.3f);

			while (timer.Running) {
				timer.Update();
				image.transform.position = Vector2.Lerp(offset, origPos, timer.Progress);
				image.color = Util.SetAlpha(image.color, timer.Progress);
				yield return new WaitForEndOfFrame();
			}

			transform.position = origPos;
			image.color = Util.SetAlpha(image.color, 1f);
		}

		private IEnumerator Lose() {

			Vector2 offset = origPos + new Vector2(0, -3f);

			SimpleTimer timer = new SimpleTimer(0.3f);

			while (timer.Running) {
				timer.Update();
				image.transform.position = Vector2.Lerp(origPos, offset, timer.Progress);
				image.color = Util.SetAlpha(image.color, 1 - timer.Progress);
				yield return new WaitForEndOfFrame();
			}

			image.transform.position = origPos;
			image.color = Util.SetAlpha(image.color, 0f);

		}

		internal void StartGain() {
			StopAllCoroutines();
			StartCoroutine(Gain());
		}

		internal void StartLose() {
			StopAllCoroutines();
			StartCoroutine(Lose());
		}

	}

}