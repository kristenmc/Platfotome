using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

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

}