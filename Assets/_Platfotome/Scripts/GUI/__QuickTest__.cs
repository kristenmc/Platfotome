using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class __QuickTest__ : MonoBehaviour {

		public AnimationClip c;
		private bool zoomed;

		private void Start() {

		}

		private void Update() {

			if (GameManager.InGameState(typeof(OverworldState))) {

				if (Input.GetKeyDown(KeyCode.T)) {
					CameraController.Instance.RequestAnimation(c);
				}

				if (Input.GetKeyDown(KeyCode.Y)) {
					CameraController.Instance.RequestReturnToTrackMode();
				}


				if (Input.GetKeyDown(KeyCode.Tab)) {
					zoomed = !zoomed;
					if (zoomed) {
						CameraController.Instance.RequestEnterMapZoom(10, Vector2.zero);
					} else {
						CameraController.Instance.RequestExitMapZoom();
					}
				}

			}

		}

	}

}