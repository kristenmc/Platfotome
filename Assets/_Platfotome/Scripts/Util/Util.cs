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

	}

}