using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome {

	public class InteractIconTrigger : MonoBehaviour {

		private static readonly Color GizmoActive = Util.FromHex("e65071");
		private static readonly Color GizmoActiveDot = Util.FromHex("ff1486");
		private static readonly Color GizmoInactive = Util.FromHex("7d636d");

		public GameObject iconPrefab;
		public enum State {
			Scanning, Visible
		}
		public State state;

		public Vector2 detectOffset = new Vector2(0, 1f);
		public Vector2 spawnOffset = new Vector2(0, 2f);

		public float radiusEnter = 3f;
		public float radiusExitDelta = 0.5f;

		private Transform iconCanvas;
		private DialogueIcon iconReference;

		private void Awake() {
			iconCanvas = GameObject.FindWithTag("IconCanvas").transform;
		}

		private void Update() {

			switch (state) {
				case State.Scanning:

					var enter = Physics2D.OverlapCircleAll(transform.position.GetXY() + detectOffset, radiusEnter);
					if (enter.Any(x => x.CompareTag("Player"))) {
						if (iconReference == null) {
							iconReference = Instantiate(iconPrefab, transform.position + (Vector3)spawnOffset, iconPrefab.transform.rotation, iconCanvas).GetComponent<DialogueIcon>();
						}
						state = State.Visible;
					}

					break;
				case State.Visible:

					var exit = Physics2D.OverlapCircleAll(transform.position.GetXY() + detectOffset, radiusEnter + radiusExitDelta);
					if (exit.All(x => !x.CompareTag("Player"))) {
						if (iconReference != null) {
							iconReference.TriggerDestroy();
							iconReference = null;
						}
						state = State.Scanning;
					}

					break;
				default:
					state = State.Scanning;
					break;
			}

		}

		private void OnDrawGizmos() {
			Gizmos.color = GizmoInactive;
			Draw(false);
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = GizmoActive;
			Draw(true);
		}

		private void Draw(bool active) {
			Gizmos.DrawWireSphere(transform.position + (Vector3)detectOffset, radiusEnter);
			Gizmos.DrawWireSphere(transform.position + (Vector3)detectOffset, radiusEnter + radiusExitDelta);
			if (active) Gizmos.color = GizmoActiveDot;
			Gizmos.DrawWireSphere(transform.position + (Vector3)spawnOffset, 0.1f);
		}

	}

}