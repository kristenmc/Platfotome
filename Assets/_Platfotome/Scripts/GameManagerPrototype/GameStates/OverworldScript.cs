using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class OverworldScript : MonoBehaviour {

		private void Awake() {
			CameraController.Instance.RequestTrackMode(transform);
		}

	}

}