using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	/// <summary>
	/// Provides general quality of life helper functions.
	/// </summary>
	public static class Util {

		/// <summary>
		/// Destroy all child gameobjects.
		/// <para><i>Anakin no</i></para>
		/// </summary>
		public static void DestroyChildGameObjects(Transform parent, bool immediate = false) {
			GameObject[] children = new GameObject[parent.childCount];
			for (int i = 0; i < parent.childCount; i++) {
				children[i] = parent.GetChild(i).gameObject;
			}
			foreach (var item in children) {
				if (immediate) {
					UnityEngine.Object.DestroyImmediate(item);
				} else {
					UnityEngine.Object.Destroy(item);
				}
			}
		}

		/// <summary>
		/// Get a Vector2 containing only the X and Y components of a Vector3.
		/// </summary>
		public static Vector2 GetXY(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

	}

}