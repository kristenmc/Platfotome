using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

	[CustomEditor(typeof(LineCameraTrack))]
	public class LineCameraTrackEditor : DraggablePointEditor {

		private void OnSceneGUI() {

			LineCameraTrack t = (LineCameraTrack)target;

			Handles.color = t.pos1 == t.pos2 ? Color.yellow : Color.cyan;
			HandleMove(ref t.pos1);
			HandleMove(ref t.pos2);

		}

	}

}