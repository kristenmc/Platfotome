using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class ChoiceWorldScript : MonoBehaviour {

		private void Awake() {
			CameraController.Instance.SetViewportMode(ViewportMode.Choiceworld);
		}

		private void OnDestroy() {
			try {
				CameraController.Instance.SetViewportMode(ViewportMode.Normal);
			} catch (MissingReferenceException) {
			}
		}

	}

}