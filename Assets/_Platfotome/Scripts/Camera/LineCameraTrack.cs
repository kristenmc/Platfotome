using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class LineCameraTrack : CameraTrackObject {

		public Vector2 pos1;
		public Vector2 pos2;

		public override Vector2 Clamp(Vector2 position) {
			Vector2 ab = pos2 - pos1;
			float t = Vector2.Dot(position - pos1, ab) / Vector2.Dot(ab, ab);
			return pos1 + Mathf.Clamp01(t) * ab;
		}

		private void OnDrawGizmos() {
			Gizmos.color = InactiveColor;
			Gizmos.DrawLine(pos1, pos2);
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = ActiveColor;
			Gizmos.DrawLine(pos1, pos2);
			Gizmos.color = ActiveSecondaryColor;
			Gizmos.DrawWireCube(pos1, CameraSize);
			Gizmos.DrawWireCube(pos2, CameraSize);
			if (pos1.x > pos2.x) {
				if (pos1.y > pos2.y) {
					Draw(-1, 1);
				} else {
					Draw(-1, -1);
				}
			} else {
				if (pos1.y > pos2.y) {
					Draw(1, 1);
				} else {
					Draw(1, -1);
				}
			}
		}

		private void Draw(float xsign, float ysign) {
			var cv = new Vector2(CameraSize.x * xsign, CameraSize.y * ysign) / 2;
			Gizmos.DrawLine(pos1 + cv, pos2 + cv);
			cv *= -1f;
			Gizmos.DrawLine(pos1 + cv, pos2 + cv);
		}

	}

}