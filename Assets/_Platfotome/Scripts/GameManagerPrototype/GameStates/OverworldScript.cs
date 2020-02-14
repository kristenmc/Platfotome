using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class OverworldScript : MonoBehaviour {

		public Transform player;

		private void Awake() {
			CameraController.Instance.RequestTrackMode(player);
		}

	}

}