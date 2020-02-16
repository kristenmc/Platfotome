﻿using System;
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
			if (parent == null) throw new ArgumentNullException("Failed to destroy child game objects, parent transform was null.");
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

		/// <summary>
		/// Loop a float between [min, max]
		/// </summary>
		public static float Loop(float value, float min, float max) {
			return (value - min) % (max - min) + min;
		}

		/// <summary>
		/// Loop a Vector2 between [min, max]
		/// </summary>
		public static Vector2 Loop(Vector2 value, Vector2 min, Vector2 max) {
			return new Vector2(Loop(value.x, min.x, max.x), Loop(value.y, min.y, max.y));
		}

	}

}