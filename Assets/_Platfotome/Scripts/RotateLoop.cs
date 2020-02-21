using UnityEngine;

namespace Platfotome {

	public class RotateLoop : MonoBehaviour {

		public float period = 5;
		public float angle = 0;
		public float swingExtents = 10;

		private void Update() {
			float a = angle + swingExtents * Mathf.Sin(Mathf.Sin(Time.time * 2 * Mathf.PI / period));
			transform.rotation = Quaternion.Euler(0, 0, a);
		}

	}

}