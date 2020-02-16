using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class OverworldScript : MonoBehaviour {

		public Transform player;
		public Canvas iconCanvas;

		private void Awake() {
			CameraController.Instance.RequestTrackMode(player);
			iconCanvas.worldCamera = CameraController.MainCamera;
		}

	}

}