using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public abstract class StackContainer : MonoBehaviour {

		public float padding = 2f;

		protected RectTransform[] children;

		/// <summary>
		/// Retrieve a child a specified index.
		/// </summary>
		public RectTransform this[int index] => children[index];

	}

}