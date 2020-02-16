using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class ParallaxBackground : MonoBehaviour {
		[SerializeField] float parallax = 0.95f;
		[SerializeField] public bool doX = true;
		[SerializeField] public bool doY = true;
		
		private Vector2 origin = Vector2.zero;

		void Awake() {
			CameraController.OnCameraMove += UpdatePosition;
			origin = transform.position;
		}

		void UpdatePosition(Vector2 position) {
			Vector2 unit = Vector2.zero;
			if (doX) unit.x = 1;
			if (doY) unit.y = 1;

			transform.position = unit * (position * parallax) + origin;
		}

		private void OnDestroy() {
			CameraController.OnCameraMove -= UpdatePosition;
		}

	}

}