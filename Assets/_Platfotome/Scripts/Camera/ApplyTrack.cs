using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class ApplyTrack : MonoBehaviour {

		public KeyCode key;

		private void Update() {
			if (Input.GetKeyDown(key)) {
				CameraController.Instance.SetCameraTrack(GetComponent<CameraTrackObject>());
			}
		}

	}

}