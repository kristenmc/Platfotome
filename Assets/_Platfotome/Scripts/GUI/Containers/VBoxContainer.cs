using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome.GUI {

	public enum VerticalAlignment {
		Top, Center, Bottom
	}

	public class VBoxContainer : StackContainer {

		public VerticalAlignment alignment = VerticalAlignment.Top;

		public GameObject debug;

		private void Awake() {
			Resize();
		}

		private void Resize() {
			children = new RectTransform[transform.childCount];

			for (int i = 0; i < transform.childCount; i++) {
				children[i] = (RectTransform)transform.GetChild(i);
			}

			float totalHeight = children.Sum(x => x.rect.height);

			Rect rect = ((RectTransform)transform).rect;
			float offset = 0;

			for (int i = 0; i < children.Length; i++) {

				switch (alignment) {

					case VerticalAlignment.Top:

						SetY(children[i], offset - children[i].rect.height / 2);
						children[i].anchorMin = new Vector2(0, 1);
						children[i].anchorMax = new Vector2(1, 1);
						offset -= children[i].rect.height + padding;

						break;
					case VerticalAlignment.Center:
					case VerticalAlignment.Bottom:
						Debug.LogWarning("Not implemented");
						break;
				}

			}

		}

		private void SetY(RectTransform obj, float y) {
			var v = obj.localPosition;
			v.y = y;
			obj.localPosition = v;
		}

	}

}