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

}