using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public abstract class CameraTrackObject : MonoBehaviour, ICameraTrack {

		protected static readonly Color ActiveColor = Color.cyan;
		protected static readonly Color ActiveSecondaryColor = Color.Lerp(Color.cyan, Color.grey, 0.6f);
		protected static readonly Color InactiveColor = Color.Lerp(Color.cyan, Color.grey, 0.85f);
		protected static readonly Vector2 CameraSize = new Vector2(19, 11);

		public abstract Vector2 Clamp(Vector2 position);
	}

}