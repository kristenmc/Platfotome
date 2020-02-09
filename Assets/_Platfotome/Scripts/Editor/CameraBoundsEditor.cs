using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

	public class DraggablePointEditor : Editor {

		public void HandleMove(ref Vector2 position) {
			float size = HandleUtility.GetHandleSize(position) * 0.13f;
			position = Handles.FreeMoveHandle(position, Quaternion.identity, size, Vector3.zero, Handles.SphereHandleCap);
		}

	}

	[CustomEditor(typeof(LineCameraTrack))]
	public class LineCameraTrackEditor : DraggablePointEditor {

		private void OnSceneGUI() {

			LineCameraTrack t = (LineCameraTrack)target;

			Handles.color = t.pos1 == t.pos2 ? Color.yellow : Color.cyan;
			HandleMove(ref t.pos1);
			HandleMove(ref t.pos2);

		}

	}

	[CustomEditor(typeof(BoxCameraTrack))]
	public class BoxCameraTrackEditor : Editor {

		private void OnSceneGUI() {

			BoxCameraTrack t = (BoxCameraTrack)target;

			Handles.color = t.min == t.max ? Color.yellow : Color.cyan;
			float size = HandleUtility.GetHandleSize(t.min) * 0.13f;

			t.min = Vector2.Min(Handles.FreeMoveHandle(t.min, Quaternion.identity, size, Vector3.zero, Handles.SphereHandleCap), t.max);
			t.max = Vector2.Max(Handles.FreeMoveHandle(t.max, Quaternion.identity, size, Vector3.zero, Handles.SphereHandleCap), t.min);

		}

	}

	internal static class LevelBoundCommands {

		[MenuItem("CONTEXT/LevelBounds/Snap Integer")]
		private static void Round(MenuCommand command) {
			LevelBounds b = (LevelBounds)command.context;
			for (int i = 0; i < b.points.Length; i++) {
				b.points[i] = new Vector2(Mathf.Round(b.points[i].x), Mathf.Round(b.points[i].y));
			}
			HandleUtility.Repaint();
		}

		[MenuItem("CONTEXT/LevelBounds/Validate")]
		private static void Validate(MenuCommand command) {
			LevelBounds b = (LevelBounds)command.context;
			bool success = true;
			for (int i = 0; i < b.Length; i++) {
				var (start, end) = b.GetPair(i);
				var d = start - end;
				if (d.x != 0 && d.y != 0) {
					Debug.LogWarning($"[LevelBounds] Pair {start} {end} not aligned");
					success = false;
				}
			}
			if (success) {
				Debug.Log("[LevelBounds] All pairs validated.");
			}
		}

	}

}