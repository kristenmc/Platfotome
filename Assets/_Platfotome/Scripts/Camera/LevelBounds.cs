using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class LevelBounds : CameraTrackObject {

		public Vector2[] points = new Vector2[0];
		public int Length => points.Length;
		public Vector2 this[int index] => points[index];

		/// <summary>
		/// Get the pair of points starting at (index, index + 1), looping around to beginning as appropriate.
		/// </summary>
		public (Vector2 start, Vector2 end) GetPair(int index) {
			if (index == points.Length - 1) {
				return (points[points.Length - 1], points[0]);
			} else {
				return (points[index], points[index + 1]);
			}
		}

		public override Vector2 Clamp(Vector2 position) {
			return position;
		}

		private void OnDrawGizmos() {
			Draw(InactiveColor);
		}

		private void Draw(Color color) {
			Gizmos.color = color;
			foreach (var pt in points) {
				Gizmos.DrawWireSphere(pt, 0.04f);
			}
			if (points.Length < 3) return;
			for (int i = 0; i < points.Length - 1; i++) {
				DrawPair(points[i], points[i + 1]);
			}
			DrawPair(points[points.Length - 1], points[0]);
		}

		private void DrawPair(Vector2 start, Vector2 end) {
			Gizmos.DrawLine(start, end);
		}

	}

}