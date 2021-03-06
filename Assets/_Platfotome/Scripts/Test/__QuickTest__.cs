﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class __QuickTest__ : MonoBehaviour {

		private InputWrapper controls;

		public AnimationClip camClip;
		private bool zoomed;

		private void Awake() {
			controls = new InputWrapper();
			controls.Enable();

			//controls.Player.Fire.performed += context => Debug.Log("Fire");
		}

		private void Start() {

		}

		private void Update() {

			if (Input.GetKeyDown(KeyCode.Keypad1)) HealthDisplay.Instance.SetHealth(0);
			if (Input.GetKeyDown(KeyCode.Keypad2)) HealthDisplay.Instance.SetHealth(1);
			if (Input.GetKeyDown(KeyCode.Keypad3)) HealthDisplay.Instance.SetHealth(2);
			if (Input.GetKeyDown(KeyCode.Keypad4)) HealthDisplay.Instance.SetHealth(3);
			if (Input.GetKeyDown(KeyCode.Keypad5)) HealthDisplay.Instance.SetHealth(4);

			if (GameManager.InGameState(typeof(OverworldState))) {

				if (Input.GetKeyDown(KeyCode.T)) {
					CameraController.Instance.RequestAnimation(camClip);
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