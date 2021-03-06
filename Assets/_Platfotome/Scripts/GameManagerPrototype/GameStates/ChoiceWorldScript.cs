﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome {

	public class ChoiceWorldScript : MonoBehaviour {

		public TextMeshProUGUI questionTextMesh;

		private void Awake() {
			CameraController.Instance.SetViewportMode(ViewportMode.Choiceworld);
			if (GameManager.StateArgs.TryGetValue(ChoiceWorldState.LevelKey, out string levelKey)) {
				Debug.Log("[ChoiceWorld] Loading level " + levelKey);
			}
			questionTextMesh.text = "Placeholder";
		}

		private void OnDestroy() {
			try {
				CameraController.Instance.SetViewportMode(ViewportMode.Normal);
			} catch (MissingReferenceException) {
			}
		}

	}

}