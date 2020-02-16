using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class ScrollTexture : MonoBehaviour {

		private Material mat;
		public Vector2 velocity = Vector2.zero;
		public Vector2 size = Vector2.one;

		private void Awake() {
			var img = GetComponent<Image>();
			img.material = mat = Instantiate(img.material);
		}

		private void Update() {
			mat.mainTextureOffset = Util.Loop(mat.mainTextureOffset - velocity / 100f * Time.deltaTime, Vector2.zero, size);
		}

	}

}