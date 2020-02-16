using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class __QuickTest__ : MonoBehaviour {

		public AnimationClip c;
		private bool zoomed;

		private void Start() {

		}

		float val;

		private void Update() {

			if (Input.GetKeyDown(KeyCode.Keypad1)) CameraController.Instance.RequestScreenShake(0.1f);
			if (Input.GetKeyDown(KeyCode.Keypad2)) CameraController.Instance.RequestScreenShake(0.3f);
			if (Input.GetKeyDown(KeyCode.Keypad3)) CameraController.Instance.RequestScreenShake(0.7f);
			if (Input.GetKeyDown(KeyCode.Keypad4)) CameraController.Instance.RequestScreenShake(1.5f);

			if (Input.GetKeyDown(KeyCode.Keypad8)) {
				Debug.Log("Disabling screenshake");
				PlayerPrefs.SetFloat(GamePreferences.Key.Screenshake, 0f);
				GamePreferences.Apply();
			}
			if (Input.GetKeyDown(KeyCode.Keypad9)) {
				Debug.Log("Enabling screenshake");
				PlayerPrefs.SetFloat(GamePreferences.Key.Screenshake, 1f);
				GamePreferences.Apply();
			}


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