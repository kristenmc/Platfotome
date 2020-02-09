using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class ScrollingTexture : MonoBehaviour {

		public Vector2 textureRepeat;
		public Vector2 velocity;

		private Vector2 currentPosition;

		private Vector2 basePosition;

		private void Awake() {
			basePosition = transform.localPosition;
		}

		private void Update() {

			currentPosition += velocity * Time.deltaTime;
			currentPosition.x %= textureRepeat.x;
			currentPosition.y %= textureRepeat.y;

			transform.localPosition = basePosition + currentPosition;

		}

	}

}