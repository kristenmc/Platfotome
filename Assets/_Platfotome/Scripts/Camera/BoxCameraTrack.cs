using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class BoxCameraTrack : CameraTrackObject {

		public Vector2 min;
		public Vector2 max;

		public override Vector2 Clamp(Vector2 position) {
			position.x = Mathf.Clamp(position.x, min.x, max.x);
			position.y = Mathf.Clamp(position.y, min.y, max.y);
			return position;
		}

		private void OnDrawGizmos() {
			Gizmos.color = InactiveColor;
			Gizmos.DrawWireCube((min + max) / 2, max - min);
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = ActiveColor;
			Gizmos.DrawWireCube((min + max) / 2, max - min);
			Gizmos.color = ActiveSecondaryColor;
			Gizmos.DrawWireCube((min + max) / 2, max - min + CameraSize);
		}

	}

}