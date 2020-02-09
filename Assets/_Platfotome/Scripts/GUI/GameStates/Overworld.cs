using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class Overworld : MonoBehaviour {

		private void Awake() {
			CameraController.Instance.trackTarget = transform;
		}

		private void OnDestroy() {
			CameraController.Instance.trackTarget = null;
		}

	}

}